namespace TimeSheet.Helper
{
    public static class Query
    {
        // SQL query to validate employee and fetch additional details
          public static string ValidateEmployeeQuery = @"
            SELECT e.EmployeeId, e.Name, e.TotalWFH, e.TotalLeaves
            FROM EmployeeLogin el
            INNER JOIN Employee e ON el.EmployeeId = e.EmployeeId
            WHERE el.EmployeeId = @EmployeeId
            AND el.Password = @Password
            AND el.IsActive = 1";

    
            // Query to get attendance for an employee
        public static string GetEmployeeAttendance = @"
            SELECT ea.AttendanceDate, ea.EmployeeID, ea.StatusId, s.StatusName
            FROM EmployeeAttendance ea
            INNER JOIN Status s ON ea.StatusId = s.StatusId
            WHERE ea.EmployeeID = @EmployeeId
            ORDER BY ea.AttendanceDate";

        // Query to add attendance
        public static string AddEmployeeAttendance = @"
            INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
            VALUES (@AttendanceDate, @EmployeeID, @StatusId)";

        // Query to get all statuses
        public static string GetAllStatuses = @"
            SELECT StatusId, StatusName FROM Status";
        
        // Query to update attendance
        public static string UpdateEmployeeAttendance = @"
            UPDATE EmployeeAttendance 
            SET StatusId = @StatusId
            WHERE EmployeeID = @EmployeeID AND AttendanceDate = @AttendanceDate";
    }

}
