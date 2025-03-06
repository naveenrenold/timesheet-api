namespace TimeSheetAPI.Helper.Query
{
    public static class Attendance
    {
        // Query to add attendance
        public static readonly string AddEmployeeAttendance = @"
            INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
            VALUES (@AttendanceDate, @EmployeeID, @StatusId);";

        //             Declare @FromDate date = '2025-02-01',
        // @ToDate date = '2025-03-01';

        // with datesCTE(dates) as 
        // (
        // 	Select @FromDate  as startingDate
        // 	union all
        // 	select DATEADD(DAY, 1, dates) from datesCTE where dates < @ToDate
        // )
        // select dates as date, ISNULL(StatusId, 0) as StatusId from datesCTE d 
        // left outer join EmployeeAttendance EA on d.dates = EA.AttendanceDate
        // left outer join Holiday H on d.dates = H.HolidayDate
        // order by dates

    }
}
