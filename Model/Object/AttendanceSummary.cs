using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Model.Object
{
    public class AttendanceSummary()
    {        
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int TotalWorkingDays { get; set; }
        public int NoOfDaysPresent { get; set; }
        public int NoOfDaysWFH { get; set; }
        public int NoOfDaysLeave { get; set; }
        public int RemainingLeaves { get; set; }
        public int RemainingWFH { get; set; }
    }
}