using Dapper;
using Microsoft.Data.SqlClient;
using TimeSheetAPI.Helper;
using TimeSheetAPI.Model.Request;

namespace TimeSheetAPI.DataLayer
{
    public class ExceptionDL
    {
        private readonly DatabaseHelper _databaseHelper;

        public ExceptionDL()
        {
            _databaseHelper = new();
        }
        public bool AddException(ExceptionRequest request)
        {
            request.ExceptionDate = request.ExceptionDate is null ? DateTime.Today : request.ExceptionDate?.Date;
            string connectionString = _databaseHelper.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Execute(Helper.Query.Exception.AddException, request);
                return result > 0;
            }
        }
    }
}