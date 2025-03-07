using Microsoft.Data.SqlClient;
using TimeSheetAPI.Helper;
using Query = TimeSheetAPI.Helper.Query;
using Dapper;
using TimeSheetAPI.Model.Object;
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

    public bool AddEmployeeAttendance(EmployeeAttendance employeeAttendance)
    {        
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Execute(Query.Attendance.AddEmployeeAttendance, employeeAttendance);
            return result > 0;
        }
    }

    public IEnumerable<string> GetAttendance(string employeeId, DateTime fromDate, DateTime toDate)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Query<string>(Query.Attendance.GetAttendance, new {employeeId, fromDate, toDate});
            return result;
        }
    }
}

