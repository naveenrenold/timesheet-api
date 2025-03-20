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

    public bool AddAttendance(EmployeeAttendance employeeAttendance)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Execute(Query.Attendance.AddEmployeeAttendance, employeeAttendance);
            return result > 0;
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
}

