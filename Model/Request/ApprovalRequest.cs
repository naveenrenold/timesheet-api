using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Model.Request
{
    public class ApprovalRequest
    {
        [Required]
        public string? ExceptionId { get; set; }
        [Required]
        public bool ApprovalStatus { get; set; }
    }
}