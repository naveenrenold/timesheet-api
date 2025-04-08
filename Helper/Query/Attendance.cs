namespace TimeSheetAPI.Helper.Query
{
    public static class Attendance
    {
        // Query to add attendance
        public static readonly string AddEmployeeAttendance = @"
            INSERT INTO EmployeeAttendance (AttendanceDate, EmployeeID, StatusId)
            VALUES (@AttendanceDate, @EmployeeID, @StatusId);";

        public static readonly string GetAttendance = @"
        with datesCTE(dates) as 
        (
        	Select @FromDate  as startingDate
        	union all
        	select DATEADD(DAY, 1, dates) from datesCTE where dates < @ToDate
        )
        select dates as date, CASE
        When DATENAME(WEEKDAY, dates) in ('Saturday', 'Sunday') Then 4
        When H.HolidayName is not NULL Then  4
        Else ISNULL(StatusId, 0) 
        End as StatusId
        from datesCTE d 
        left outer join EmployeeAttendance EA on d.dates = EA.AttendanceDate and EA.EmployeeID = @EmployeeId
        left outer join Holiday H on d.dates = H.HolidayDate        
        order by dates
        OPTION (MAXRECURSION 500)
        ";

        public static readonly string GetAttendanceSummary = @"
         Declare 
@TotalDays int = 0,
@StartingDay int = 0,
@TotalWorkingDays int = 0,
@Weeks int = 0,
@ExtraDaysThatAreNotACompleteWeek int = 0,
@ExtraDays int = 0,
@HasASaturday int = 0,
@HasASunday int = 0,
@Holiday int = 0;

Select @TotalDays = DATEDIFF(WEEKDAY, @fromDate, @toDate) + 1;
Select @StartingDay = (DATEPART(WEEKDAY, @fromDate) + 5) % 7 ;  
Select @Weeks = (@TotalDays / 7) ;
Select @ExtraDaysThatAreNotACompleteWeek = @TotalDays % 7;
Select @HasASaturday = (@ExtraDaysThatAreNotACompleteWeek + @StartingDay) / 6
Select @HasASunday = (@ExtraDaysThatAreNotACompleteWeek + @StartingDay) / 7

Select @ExtraDays = @ExtraDaysThatAreNotACompleteWeek - @HasASaturday - @HasASunday

Select @TotalWorkingDays = (@Weeks * 5) + IIF(@ExtraDays > 0 , @ExtraDays , 0) 

Select @Holiday = Count(*) from Holiday where HolidayDate BETWEEN @fromDate and @toDate

-- Select @TotalDays AS TotalDays,
-- @StartingDay AS StartingDay,
-- @TotalWorkingDays AS TotalWorkingDays,
-- @Weeks AS Weeks,
-- @ExtraDaysThatAreNotACompleteWeek AS ExtraDaysThatAreNotACompleteWeek,
-- @ExtraDays AS ExtraDays,
-- @HasASaturday AS HasASaturday,
-- @HasASunday AS HasASunday

Select 
MAX(E.EmployeeID) as EmployeeId, MAX(E.Name) as EmployeeName,
@TotalWorkingDays - @Holiday as TotalWorkingDays,
sum(CASE EA.StatusId When '1' Then 1 Else 0 END) as NoOfDaysPresent,
sum(CASE EA.StatusId When '2' Then 1 Else 0 END) as NoOfDaysWFH,
sum(CASE EA.StatusId When '3' Then 1 Else 0 END) as NoOfDaysLeave,
max(E.TotalLeaves) as RemainingLeaves,
max(E.TotalWFH) as RemainingWFH
from Employee E
inner join HR on HR.ReportingEmployeeId = E.EmployeeID and HR.EmployeeId = @employeeId
left outer join EmployeeAttendance EA on E.EmployeeId = EA.EmployeeID and  EA.AttendanceDate BETWEEN @fromDate and @toDate
group by E.EmployeeID";

    }
}
