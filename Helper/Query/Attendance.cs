namespace TimeSheetAPI.Helper.Query
{
    public static class Attendance
    {
        // Query to add attendance
        public static readonly string AddEmployeeAttendance = @"
            INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
            VALUES (@AttendanceDate, @EmployeeID, @StatusId);";        
    }
}
