using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Model.Object;
using TimeSheetAPI.Helper.Constants;

namespace TimeSheetAPI.Controller;

[Route("api/attendance")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceDL _attendanceDL = new();

    [HttpPost]
    public IActionResult AddAttendance([FromBody] EmployeeAttendance employeeAttendance)
    {
        var Attendance = _attendanceDL.GetAttendance(employeeAttendance.EmployeeId.ToString(), employeeAttendance.AttendanceDate, employeeAttendance.AttendanceDate);
        var error = ValidatePrevAttendance(Attendance);
        if (error != null)
        {
            return BadRequest(error);
        }
        var attendance = _attendanceDL.AddAttendance(employeeAttendance);
        if (attendance < 0)
        {
            return Ok(new { message = "Attendance added successfully!" });
        }
        return StatusCode(500, new { message = Constants.AddAttendanceErrorList[attendance] });
    }

    [HttpGet]
    public IActionResult GetAttendance(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        var error = ValidateGetAttendance(employeeId, fromDate, toDate);
        if (error.Any())
        {
            return BadRequest(new { Error = error });
        }
        var response = _attendanceDL.GetAttendance(employeeId, fromDate, toDate);
        if (response.Any())
        {
            return Ok(response);
        }
        return StatusCode(500, "No data returned");
    }

    [HttpGet]
    [Route("report")]
    public IActionResult GetReport(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        var response = _attendanceDL.GetReport(employeeId, fromDate, toDate);
        if(response.Any())
        {
            return Ok(response);
        }
        return StatusCode(500, "Attendance report data could not be loaded");
    }

    [NonAction]
    public IEnumerable<ApiError> ValidateGetAttendance(string employeeId, DateTime? fromDate, DateTime? toDate)
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            yield return new ApiError("EmployeeId is a mandatory field");
        }
        if (fromDate != null && fromDate > DateTime.Now && fromDate > DateTime.Now.AddYears(-1))
        {
            yield return new ApiError("From Date cannot be greater than current date");
        }
        if (fromDate != null && fromDate < DateTime.Now.AddYears(-1))
        {
            yield return new ApiError("From Date cannot be older than 1 year");
        }
        if (toDate != null && toDate?.Month > DateTime.Now.Month)
        {
            yield return new ApiError("To Date cannot be greater than current month");
        }
    }
    [NonAction]
    public ApiError? ValidatePrevAttendance(IEnumerable<Model.Response.AttendanceStatus>? attendance)
    {
        if (attendance != null && !attendance.Any())
        {
            return new ApiError("Failed to get attendance.");
        }
        if (attendance!.FirstOrDefault()!.StatusId != 0)
        {
            var statusName = (Status)attendance!.FirstOrDefault()!.StatusId;
            return new ApiError($"Status is already present as \"{statusName}\" for selected date.");
        }
        return null;
    }
}
