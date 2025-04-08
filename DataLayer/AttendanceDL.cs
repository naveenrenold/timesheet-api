using Microsoft.Data.SqlClient;
using TimeSheetAPI.Helper;
using Query = TimeSheetAPI.Helper.Query;
using Dapper;
using TimeSheetAPI.Model.Object;
using TimeSheetAPI.Model.Response;
namespace TimeSheetAPI.DataLayer;

public class AttendanceDL
{
    private readonly DatabaseHelper _databaseHelper = new();
    private readonly string connectionString;

    //string connectionString = _databaseHelper.GetConnectionString();
    public AttendanceDL()
    {
        connectionString = _databaseHelper.GetConnectionString();
    }

    public int AddAttendance(EmployeeAttendance employeeAttendance)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            // Start transaction
            using (var transaction = connection.BeginTransaction())
            {
                // Update WFH balance if status is WFH (2)
                if (employeeAttendance.StatusId == 2)
                {
                    int rowsAffected = connection.Execute(Query.Employee.UpdateWfhBalance, employeeAttendance, transaction);
                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return 0; // No WFH balance left to reduce
                    }
                }
                // Update Leave balance if status is Leave (3)
                else if (employeeAttendance.StatusId == 3)
                {
                    int rowsAffected = connection.Execute(Query.Employee.UpdateLeaveBalance, employeeAttendance, transaction);
                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return 0; // No leave balance left to reduce
                    }
                }
                var result = connection.Execute(Query.Attendance.AddEmployeeAttendance, employeeAttendance, transaction);
                if (result == 0)
                {
                    // Commit transaction
                    transaction.Rollback();
                    return 1;
                }
                transaction.Commit();
                return -1;
            }
        }
    }

    public IEnumerable<AttendanceStatus> GetAttendance(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        fromDate = fromDate is null ? DateTime.Today.AddMonths(-4) : fromDate?.Date;
        toDate = toDate is null ? DateTime.Today : toDate?.Date;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Query<AttendanceStatus>(Query.Attendance.GetAttendance, new { employeeId, fromDate, toDate });
            return result;
        }
    }

    public IEnumerable<AttendanceSummary> GetReport(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        var currentDate = DateTime.UtcNow.AddMinutes(330);
        var currentYear = currentDate.Year;
        var currentMonth = currentDate.Month;
        var currentDay = currentDate.Day;
        fromDate = fromDate is null ? new DateTime(currentYear, currentMonth, 1) : fromDate?.Date;
        toDate = toDate is null ? new DateTime(currentYear, currentMonth, currentDay) : toDate?.Date;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Query<AttendanceSummary>(Query.Attendance.GetAttendanceSummary, new { employeeId, fromDate, toDate });
            return result;
        }
    }
}

