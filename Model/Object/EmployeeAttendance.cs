using System.ComponentModel.DataAnnotations;

namespace TimeSheetAPI.Model.Object
{
    public class EmployeeAttendance
    {
        [Required]
        [Range(typeof(DateTime), DateTime.Now.AddYears(-1).ToString(), new DateTime(DateTime.Now.Year, DateTime.Now.Month +1, -1).ToString())]
        public DateTime AttendanceDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public int? StatusId { get; set; }

    }
}