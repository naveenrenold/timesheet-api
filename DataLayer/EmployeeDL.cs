namespace TimeSheet.DataLayer
{
    using TimeSheet.Helper; // Import the Query class
    using TimeSheet.Models; 
    using Microsoft.Data.SqlClient;
    using Dapper;

    public class EmployeeDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public EmployeeDL(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // Change return type to Employee? to allow for null when no employee is found
        public Employee? ValidateEmployee(string employeeId, string password)
        {
            string connectionString = _databaseHelper.GetConnectionString(); // Get the connection string from DatabaseHelper
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();                
                var result = connection.QueryFirstOrDefault<Employee>(Query.ValidateEmployeeQuery, new {EmployeeId = employeeId, Password = password });
                return result;
                // using (var command = new SqlCommand(Query.ValidateEmployeeQuery, connection)) // Use the query from Query.cs
                // {
                //     command.Parameters.AddWithValue("@EmployeeId", employeeId);
                //     command.Parameters.AddWithValue("@Password", password);

                //     using (var reader = command.ExecuteReader())
                //     {
                //         if (reader.Read())
                //         {
                //             return new Employee
                //             {
                //                 EmployeeId = reader.GetInt32(0),
                //                 Name = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1), // Default to "Unknown" if Name is null
                //                 TotalWFH = reader.GetInt32(2),
                //                 TotalLeaves = reader.GetInt32(3)
                //             };
                //         }
                //     }
                // }
            }

           // return null; // Return null if no matching employee found
        }
    }
}
