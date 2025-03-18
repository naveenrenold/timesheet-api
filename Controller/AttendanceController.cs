using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Model.Object;

namespace TimeSheetAPI.Controller;

[Route("api/attendance")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceDL _attendanceDL = new();

    [HttpPost]
    public IActionResult AddAttendance([FromBody] EmployeeAttendance employeeAttendance)
    {
        try
        {
            employeeAttendance.StatusId ??= 1;
            var prevAttendance = _attendanceDL.GetAttendance(employeeAttendance.EmployeeId.ToString(), employeeAttendance.AttendanceDate, employeeAttendance.AttendanceDate);
            var attendance = false;
            if (prevAttendance != null && !prevAttendance.Any())
            {
                return BadRequest(new { message = "Failed to add attendance." });
            }
            if (prevAttendance!.FirstOrDefault()!.StatusId != 0)
            {
                var statusName = (Status)prevAttendance!.FirstOrDefault()!.StatusId;
                return BadRequest(new { message = $"Status is already present as \"{statusName}\" for selected date." });
            }
            attendance = _attendanceDL.AddAttendance(employeeAttendance);
            if (attendance)
            {
                return Ok(new { message = "Attendance added successfully.", attendance });
            }

            return BadRequest(new { message = "Failed to add attendance." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }

    [HttpGet]
    public IActionResult GetAttendance(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            return BadRequest("EmployeeId is a mandatory field");
        }
        if (fromDate != null && fromDate > DateTime.Now)
        {
            return BadRequest("From Date cannot be greater than current date");
        }
        if (toDate != null && toDate?.Month > DateTime.Now.Month)
        {
            return BadRequest("To Date cannot be greater than current month");
        }

        var response = _attendanceDL.GetAttendance(employeeId, fromDate, toDate);
        if (response.Any())
        {
            return Ok(response);
        }
        return StatusCode(500, "No data returned");
    }
}
