
namespace TimeSheet.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }  // Make Name nullable
        public int TotalWFH { get; set; }
        public int TotalLeaves { get; set; }
    }
}