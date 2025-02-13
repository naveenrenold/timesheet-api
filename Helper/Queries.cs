namespace TimeSheet.Helper
{
    public static class Query
    {
        // SQL query to validate employee and fetch additional details
        public static string ValidateEmployeeQuery = @"
            SELECT e.EmployeeId, e.Name, e.TotalWFH, e.TotalLeaves, e.Specialization, e.Gender
            FROM EmployeeLogin el
            INNER JOIN Employee e ON el.EmployeeId = e.EmployeeId
            WHERE el.EmployeeId = @EmployeeId
            AND el.Password = @Password
            AND el.IsActive = 1";

        // Query to add attendance
        public static string AddEmployeeAttendance = @"
            INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
            VALUES (@AttendanceDate, @EmployeeID, @StatusId);";

        // Query to update WFH balance
        public static string UpdateWfhBalance = @"
            UPDATE Employee
            SET TotalWFH = TotalWFH - 1
            WHERE EmployeeId = @EmployeeId AND TotalWFH > 0;";

        // Query to update Leave balance
        public static string UpdateLeaveBalance = @"
            UPDATE Employee
            SET TotalLeaves = TotalLeaves - 1
            WHERE EmployeeId = @EmployeeId AND TotalLeaves > 0;";
        
        public static string GetEmployeeById = @"
    SELECT e.EmployeeId, e.Gender 
    FROM Employee e 
    WHERE e.EmployeeId = @EmployeeId";

            
    }
}
