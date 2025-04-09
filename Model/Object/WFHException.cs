namespace TimeSheetAPI.Model.Object
{
    public class WFHException
    {
        public string? ExceptionId { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ExceptionDate { get; set; }
        public string? Reason { get; set; }
    }
}