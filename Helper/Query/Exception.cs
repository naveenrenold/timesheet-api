namespace TimeSheetAPI.Helper.Query
{
    public static class Exception
    {
        public static readonly string AddException = @"
IF EXISTS(
    (Select  1 from Employee E 
    left outer join WFHException WFH on E.EmployeeId = WFH.ReportingToEmployeeId and WFH.ExceptionDate = @ExceptionDate
where E.EmployeeId = @ReportingToEmployeeId and E.RoleId > 1 and WFH.ExceptionDate is NULL))
Insert into WFHException VALUES (@ExceptionDate, GETDATE(),@EmployeeId,@ReportingToEmployeeId,0, @Reason ) 
";
    }
}

