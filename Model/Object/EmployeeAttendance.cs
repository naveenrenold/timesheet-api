using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Validations;

namespace TimeSheetAPI.Model.Object
{
    public class EmployeeAttendance : IValidatableObject
    {
        [Required]        
        public DateTime AttendanceDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [EnumDataType(typeof(Status))]
        public int? StatusId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {               
            if(DateTime.Now.CompareTo(AttendanceDate.Date) > 0)
            {
                yield return new ValidationResult(errorMessage: "Date cannot be a future date", [nameof(AttendanceDate)]);
            }
            if(AttendanceDate.Date.CompareTo(DateTime.Now.AddDays(-7)) <= 0)
            {
                yield return new ValidationResult(errorMessage: "Date cannot be older than 7 days", [nameof(AttendanceDate)]);
            }
        }

    }
}