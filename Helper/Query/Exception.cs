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

        public static readonly string GetException = @"
        Select WFH.ExceptionId, E.EmployeeId, E.Name as EmployeeName, WFH.ExceptionDate, WFH.Reason from WFHException WFH
 inner join Employee E on WFH.EmployeeId = E.EmployeeId
 where ReportingToEmployeeId = @employeeId and ApprovalId = 0 order by ExceptionId        
        ";
        public static readonly string ApproveException = @"
        Update WFHException set ApprovalId = @approvalStatus where ExceptionId = @exceptionId 
        ";
    }
}

