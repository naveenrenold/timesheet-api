namespace TimeSheetAPI.Model.Object
{
    public class AttendanceUpdateRequest
    {
        public string? EmployeeId { get; set; }
        public int StatusId { get; set; }  // 2 for WFH, 3 for Leave
    }
}
