namespace TimeSheetAPI.Model.Object
{
    public class AttendanceUpdateRequest
    {
        public string? EmployeeId { get; set; }
        public int StatusId { get; set; }  // 2 for WFH, 3 for Leave


        //  public string? Name { get; set; }  
        // public Employee? Specialization {get; set;}
        // public Employee? Gender {get; set;}

        // public Employee? TotalWFH { get; set; }
        // public Employee? TotalLeaves { get; set; }
    }
}
