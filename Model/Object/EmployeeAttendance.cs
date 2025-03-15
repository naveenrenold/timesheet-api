using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Validations;

namespace TimeSheetAPI.Model.Object
{
    public class EmployeeAttendance : IValidatableObject
    {
        [Required(ErrorMessage = "AttendanceDate is required")]
        [DataType(DataType.DateTime)]
        public DateTime AttendanceDate { get; set; }

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }

        [EnumDataType(typeof(Status), ErrorMessage = "StatusId must be a valid value")]
        public int? StatusId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AttendanceDate.Date.CompareTo(DateTime.Now) > 0)
            {
                yield return new ValidationResult(errorMessage: "Date cannot be a future date", [nameof(AttendanceDate)]);
            }
            if (AttendanceDate.Date.CompareTo(DateTime.Now.AddDays(-8)) < 0)
            {
                yield return new ValidationResult(errorMessage: "Date cannot be older than 7 days", [nameof(AttendanceDate)]);
            }
        }

    }
}