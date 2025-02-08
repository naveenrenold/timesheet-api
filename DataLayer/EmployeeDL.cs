namespace TimeSheet.DataLayer
{
    public class EmployeeDL
    {
        private List<string> employeeIds = new List<string> { "1000"};
        public  bool ValidateEmployee(string employeeId)
        {
            return employeeIds.Contains(employeeId);
        }
    }
}