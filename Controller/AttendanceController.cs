using Microsoft.AspNetCore.Mvc;
using TimeSheet.DataLayer;
using TimeSheet.Models;
using System;
using System.Collections.Generic;

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

        // 1. Get attendance records for an employee
        [HttpGet("employee/{employeeId}")]
        public IActionResult GetEmployeeAttendance(int employeeId)
        {
            try
            {
                var attendanceList = _attendanceDL.GetEmployeeAttendance(employeeId);
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    return NotFound(new { message = "No attendance records found for this employee." });
                }

                return Ok(attendanceList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // 2. Get all available attendance statuses (e.g., Present, Leave, WFH, etc.)
        [HttpGet("statuses")]
        public IActionResult GetStatuses()
        {
            try
            {
                var statuses = _attendanceDL.GetStatuses();
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // 3. Add attendance for a specific day
        [HttpPost("attendance")]
        public IActionResult AddEmployeeAttendance([FromBody] EmployeeAttendance attendance)
        {
            try
            {
                if (attendance == null)
                {
                    return BadRequest(new { message = "Invalid attendance data." });
                }

                _attendanceDL.AddEmployeeAttendance(attendance);
                return CreatedAtAction(nameof(GetEmployeeAttendance), new { employeeId = attendance.EmployeeId }, attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // 4. Update attendance status for a specific day
        [HttpPut("attendance")]
        public IActionResult UpdateEmployeeAttendance([FromBody] EmployeeAttendance attendance)
        {
            try
            {
                if (attendance == null)
                {
                    return BadRequest(new { message = "Invalid attendance data." });
                }

                _attendanceDL.UpdateEmployeeAttendance(attendance);
                return Ok(new { message = "Attendance updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
