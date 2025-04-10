using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Model.Request
{
    public class ExceptionRequest
    {
        [Required]
        public string? EmployeeId { get; set; }
        public string? Reason { get; set; }
        [Required]
        public DateTime? ExceptionDate { get; set; }
        [Required]
        public string? ReportingToEmployeeId { get; set; }

    }
}