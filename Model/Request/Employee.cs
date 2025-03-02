namespace TimeSheetAPI.Model.Request
{
    public class LoginRequest
    {
        public string? EmployeeId { get; set; }
        public string? Password { get; set; }
    }
}