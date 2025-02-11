using Microsoft.AspNetCore.Mvc;
using TimeSheet.DataLayer;
using TimeSheet.Models;
using System;

namespace TimeSheet.Controllers
{
    [Route("api/attendance")]  
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceDL _attendanceDL;

        public AttendanceController(AttendanceDL attendanceDL)
        {
            _attendanceDL = attendanceDL;
        }

        [HttpPost("attendance")]
        public IActionResult AddEmployeeAttendance([FromBody] EmployeeAttendance employeeAttendance)
        {
            try
            {
                if (employeeAttendance == null)
                {
                    return BadRequest(new { message = "Invalid attendance data." });
                }

                // Call AddEmployeeAttendance in Data Layer with DateOnly
                var attendance = _attendanceDL.AddEmployeeAttendance(
                    employeeAttendance.AttendanceDate,  // DateOnly here
                    employeeAttendance.EmployeeId, 
                    employeeAttendance.StatusId ?? 1 // Default to 1 if StatusId is null
                );

                if (attendance != null)
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
