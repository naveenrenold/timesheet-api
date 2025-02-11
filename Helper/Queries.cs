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

    
            
        

        // Query to add attendance
        public static string AddEmployeeAttendance = @"
    INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
    VALUES (@AttendanceDate, @EmployeeID, @StatusId);";


        


}}
