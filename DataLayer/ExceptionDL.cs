using Dapper;
using Microsoft.Data.SqlClient;
using TimeSheetAPI.Helper;
using TimeSheetAPI.Model.Object;
using TimeSheetAPI.Model.Request;

namespace TimeSheetAPI.DataLayer
{
    public class ExceptionDL
    {
        private readonly DatabaseHelper _databaseHelper;
        private readonly string _connectionString;

        public ExceptionDL()
        {
            _databaseHelper = new();
            _connectionString = _databaseHelper.GetConnectionString();
        }
        public bool AddException(ExceptionRequest request)
        {
            request.ExceptionDate = request.ExceptionDate is null ? DateTime.Today : request.ExceptionDate?.Date;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Execute(Helper.Query.Exception.AddException, request);
                return result > 0;
            }
        }
        public IEnumerable<WFHException> GetException(string employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query<WFHException>(Helper.Query.Exception.GetException, new { employeeId });
                return result;
            }
        }

        public bool ApproveException(string exceptionId, bool approvalStatus)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Execute(Helper.Query.Exception.ApproveException, new { exceptionId, approvalStatus = approvalStatus ? 1 : 2 });
                return result > 0;
            }
        }
    }
}