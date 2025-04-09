using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Model.Request;
namespace TimeSheetAPI.Controller;
[Route("api/exception")]
[ApiController]
public class ExceptionController : ControllerBase
{
  private readonly ExceptionDL exceptionDL = new();
  private readonly AttendanceDL attendanceDL = new();
  [HttpPost]
  public IActionResult AddException([FromBody] ExceptionRequest request)
  {
    if (request.EmployeeId == request.ReportingToEmployeeId)
    {
      return BadRequest(new { message = "Employee ID and Reporting To Employee ID cannot be the same" });
    }
    var statuslist = attendanceDL.GetAttendance(request.EmployeeId!, request.ExceptionDate, request.ExceptionDate);
    var status = statuslist.FirstOrDefault();
    if (status == null)
    {
      return StatusCode(400, new { message = "Failed to create exception" });
    }
    if (status.StatusId != 0)
    {
      return StatusCode(400, new { message = "Status is already " + status.StatusId });
    }
    var response = exceptionDL.AddException(request);
    if (response)
    {
      return Ok(new { message = "Exception filed successfully" });
    }
    return StatusCode(400, new { message = "Failed to create exception" });
  }

  [HttpGet]
  public IActionResult GetException([Required] string employeeId)
  {
    if (string.IsNullOrEmpty(employeeId))
    {
      return BadRequest(new { message = "Employee ID is required" });
    }
    var response = exceptionDL.GetException(employeeId);
    return Ok(response);
  }

  [HttpPost]
  [Route("approve")]
  public IActionResult ApproveException([FromBody][Required] ApprovalRequest request)
  {
    if (string.IsNullOrEmpty(request.ExceptionId))
    {
      return BadRequest(new { message = "Exception ID is required" });
    }
    var response = exceptionDL.ApproveException(request.ExceptionId, request.ApprovalStatus);
    if (!response)
    {
      return BadRequest(new { message = "Failed to update exception" });
    }
    return Ok(new { message = "Exception updated successfully" });
  }
}