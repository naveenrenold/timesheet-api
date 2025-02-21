namespace TimeSheet.DataLayer
{
    using TimeSheet.Models;
    using TimeSheet.Helper; // Import the Query class
    using Microsoft.Data.SqlClient;
    using Dapper;

    public class EmployeeDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public EmployeeDL(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public Employee? ValidateEmployee(string employeeId, string password)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<Employee>(Query.ValidateEmployeeQuery, new { EmployeeId = employeeId, Password = password });
                return result;
            }
        }

        // Method to update employee WFH or leave balance based on attendance status
        public bool UpdateEmployeeBalance(string employeeId, int statusId)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                // Start transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update WFH balance if status is WFH (2)
                        if (statusId == 2)
                        {
                            int rowsAffected = connection.Execute(Query.UpdateWfhBalance, new { EmployeeId = employeeId }, transaction);
                            if (rowsAffected == 0) return false; // No WFH balance left to reduce
                        }
                        // Update Leave balance if status is Leave (3)
                        else if (statusId == 3)
                        {
                            int rowsAffected = connection.Execute(Query.UpdateLeaveBalance, new { EmployeeId = employeeId }, transaction);
                            if (rowsAffected == 0) return false; // No leave balance left to reduce
                        }

                        // Commit transaction
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback transaction in case of error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Employee? GetEmployeeById(string employeeId)
       {
             string connectionString = _databaseHelper.GetConnectionString();
             using (var connection = new SqlConnection(connectionString))
          {
             return connection.QueryFirstOrDefault<Employee>( Query.GetEmployeeById,  new { EmployeeId = employeeId });
          }
           
        }

  



    
    }
}
