using Microsoft.Data.SqlClient;
using Dapper;
using TimeSheetAPI.Model.Object;
using TimeSheetAPI.Helper;
using Query = TimeSheetAPI.Helper.Query;
namespace TimeSheetAPI.DataLayer
{
    public class EmployeeDL
    {
        private readonly DatabaseHelper _databaseHelper;
        private readonly string connectionString;

        public EmployeeDL()
        {
            _databaseHelper = new();
            connectionString = _databaseHelper.GetConnectionString();
        }

        public Employee? ValidateEmployee(string employeeId, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<Employee>(Query.Employee.ValidateEmployeeQuery, new { EmployeeId = employeeId, Password = password });
                return result;
            }
        }

        public Employee? GetEmployeeById(string employeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Employee>(Query.Employee.GetEmployeeById, new { EmployeeId = employeeId });
            }

        }
    }
}
