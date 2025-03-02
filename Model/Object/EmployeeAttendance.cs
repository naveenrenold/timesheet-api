namespace TimeSheetAPI.Model.Object
{
    public class EmployeeAttendance
    {
        public DateTime AttendanceDate { get; set; }
        public int EmployeeId { get; set; }
        public int? StatusId { get; set; }

        public Status? Status { get; set; }
    }
}