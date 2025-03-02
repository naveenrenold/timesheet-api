using Microsoft.Data.SqlClient;
using TimeSheetAPI.Helper;
using Query = TimeSheetAPI.Helper.Query;
using Dapper;
using TimeSheetAPI.Model.Object;
namespace TimeSheetAPI.DataLayer;

public class AttendanceDL()
{
    private readonly DatabaseHelper _databaseHelper = new();

    public bool AddEmployeeAttendance(EmployeeAttendance employeeAttendance)
    {
        string connectionString = _databaseHelper.GetConnectionString();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var result = connection.Execute(Query.Attendance.AddEmployeeAttendance, employeeAttendance);
            return result > 0;
        }
    }
}

