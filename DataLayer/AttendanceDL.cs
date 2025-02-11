namespace TimeSheet.DataLayer
{
    using TimeSheet.Models;
    using TimeSheet.Helper;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Data;

    public class AttendanceDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public AttendanceDL(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public EmployeeAttendance? AddEmployeeAttendance(DateTime attendanceDate, int employeeId, int statusId)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Query.AddEmployeeAttendance, connection))
                {
                    // Convert DateOnly to DateTime (time is set to midnight)
                   // DateTime attendanceDateTime = attendanceDate.ToDateTime(TimeOnly.MinValue);
                    
    
                    
                    // Add parameters for SQL Server
                    command.Parameters.AddWithValue("@AttendanceDate", attendanceDate); // DateTime here
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StatusId", statusId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
    
                         
                            return new EmployeeAttendance
                            {
                               AttendanceDate = reader.GetDateTime(1), // Convert back to DateOnly
                               EmployeeId = reader.GetInt32(1),
                               StatusId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2) // Null check for StatusId
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
