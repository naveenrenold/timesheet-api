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
    var statuslist = attendanceDL.GetAttendance(request.EmployeeId!, request.ExceptionDate, request.ExceptionDate);
    var status = statuslist.FirstOrDefault();
    if (status == null)
    {
      return StatusCode(500, "Exception was not updated");
    }
    if (status.StatusId != 0)
    {
      return StatusCode(500, "Status is already " + status.StatusId);
    }
    var response = exceptionDL.AddException(request);
    if (response)
    {
      return Ok("Exception filed successfully");
    }
    return StatusCode(500, "Exception was not updated");
  }

}