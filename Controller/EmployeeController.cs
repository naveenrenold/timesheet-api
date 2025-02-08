namespace TimeSheet.Controller
{
    using System.Data;
    using System.Net;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using TimeSheet.DataLayer;  
    using TimeSheet.Models;   

   [Route("api/employee")]
   [ApiController]
    public class EmployeeController : ControllerBase
    //1.Query https://localhost:422272/validateemployee?employeid=101
    {        //2.Route [Route("validateemployee/{employeeId}")]  https://localhost:422272/validateemployee/101
             //3. Body (only for POST method). use [Frombody]
        //readonly EmployeeDL employeeDL = new();
        private readonly EmployeeDL _employeeDL;

        // Constructor injection
        public EmployeeController(EmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }
        // [HttpGet]
        // [Route("validateemployee/{employeeId}")]
        // public IActionResult ValidateEmployee(string employeeId)
        // {
        //     if (string.IsNullOrEmpty(employeeId))
        //     {
        //         return BadRequest("Employee ID is required.");
        //     }
        //      bool isValid = _employeeDL.ValidateEmployee(employeeId);
        //     return Ok(isValid);            
        // }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
           try
    {
        var employee = _employeeDL.ValidateEmployee(loginRequest.EmployeeId!, loginRequest.Password!);
        if (employee != null)
        {
            return Ok(new { name = employee.Name });
        }
        return Unauthorized(new { message = "Invalid credentials" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
    }
    }
 
} }