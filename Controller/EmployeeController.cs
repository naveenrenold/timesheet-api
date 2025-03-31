using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Model.Object;
using TimeSheetAPI.Model.Request;

namespace TimeSheetAPI.Controller;
[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeDL _employeeDL;

    // Constructor injection
    public EmployeeController()
    {
        _employeeDL = new();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var employee = _employeeDL.ValidateEmployee(loginRequest.EmployeeId!, loginRequest.Password!);
            if (employee != null)
            {
                return Ok(new
                {
                    name = employee.Name,
                    employeeId = employee.EmployeeId,
                    totalWFH = employee.TotalWFH,
                    totalLeaves = employee.TotalLeaves,
                    specialization = employee.Specialization,
                    gender = employee.Gender
                });
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }
}
