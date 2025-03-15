namespace TimeSheetAPI.Helper.Query
{
    public static class Exception
    {
        public static readonly string AddException = @"

IF ((Select COUNT(*) From Employee where EmployeeId = @ReportingToEmployeeId and RoleId > 1)>0)
BEGIN 
Insert into WFHException VALUES (@ExceptionDate, GETDATE(),@EmployeeId,@ReportingToEmployeeId,0, @Reason )
END
";
    }
}