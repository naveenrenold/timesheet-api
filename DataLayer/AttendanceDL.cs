namespace TimeSheet.DataLayer
{
    using TimeSheet.Models;
    using TimeSheet.Helper;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Data;
    using Dapper;

    public class AttendanceDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public AttendanceDL(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public bool AddEmployeeAttendance(EmployeeAttendance employeeAttendance)
        {
            string connectionString = _databaseHelper.GetConnectionString();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Execute(Query.AddEmployeeAttendance, employeeAttendance);   
                return result > 0;             
            }            
        }
    }
}
