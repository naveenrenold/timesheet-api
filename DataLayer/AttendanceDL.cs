namespace TimeSheet.DataLayer
{
    using TimeSheet.Helper; // Import the Query class
    using TimeSheet.Models;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    public class AttendanceDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public AttendanceDL(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

               public List<EmployeeAttendance> GetEmployeeAttendance(int employeeId)
        {
            List<EmployeeAttendance> attendanceList = new List<EmployeeAttendance>();
            string connectionString = _databaseHelper.GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Query.GetEmployeeAttendance, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceList.Add(new EmployeeAttendance
                            {
                                AttendanceDate = reader.GetDateTime(0),
                                EmployeeId = reader.GetInt32(1),
                                StatusId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
        
                                Status = reader.IsDBNull(3) ? null : new Status { StatusId = reader.GetInt32(2), StatusName = reader.GetString(3) }
                            });
                        }
                    }
                }
            }

            return attendanceList;
        }

        
        public void AddEmployeeAttendance(EmployeeAttendance attendance)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Query.AddEmployeeAttendance, connection))
                {
                    command.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
                    command.Parameters.AddWithValue("@EmployeeID", attendance.EmployeeId);
                    
                    command.Parameters.AddWithValue("@StatusId", attendance.StatusId ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        
        public void UpdateEmployeeAttendance(EmployeeAttendance attendance)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Query.UpdateEmployeeAttendance, connection))
                {
                    command.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
                    command.Parameters.AddWithValue("@EmployeeID", attendance.EmployeeId);
                    // Null-safe handling of StatusId
                    command.Parameters.AddWithValue("@StatusId", attendance.StatusId ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        
        public List<Status> GetStatuses()
        {
            List<Status> statusList = new List<Status>();
            string connectionString = _databaseHelper.GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Query.GetAllStatuses, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusList.Add(new Status
                            {
                                StatusId = reader.GetInt32(0), 
                                StatusName = reader.IsDBNull(1) ? null : reader.GetString(1) 
                            });
                        }
                    }
                }
            }

            return statusList;
        }
    }
}
