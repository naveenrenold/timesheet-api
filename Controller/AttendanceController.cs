using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Model.Object;

namespace TimeSheetAPI.Controller
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController(AttendanceDL attendanceDL) : ControllerBase
    {
        private readonly AttendanceDL _attendanceDL = attendanceDL;

        [HttpPost("attendance")]
        public IActionResult AddEmployeeAttendance([FromBody] EmployeeAttendance employeeAttendance)
        {
            try
            {
                if (employeeAttendance == null)
                {
                    return BadRequest(new { message = "Invalid attendance data." });
                }
                employeeAttendance.StatusId ??= 1;
                var attendance = _attendanceDL.AddEmployeeAttendance(employeeAttendance);

                if (attendance)
                    return Ok(new { message = "Attendance added successfully.", attendance });

                return BadRequest(new { message = "Failed to add attendance." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
