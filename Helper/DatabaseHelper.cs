namespace TimeSheetAPI.Helper
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            // var config = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .Build();

            // _connectionString = "Server=tcp:time-sheet.database.windows.net,1433;Initial Catalog=main;Persist Security Info=False;User ID=naveenrenold;Password=CHRisty1002@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            // _connectionString = "Server=tcp:time-sheet.database.windows.net 1433 -d main -Gt,1433;Initial Catalog=main;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
            _connectionString = "Server=tcp:time-sheet.database.windows.net,1433;Initial Catalog=main;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

        }
        public string GetConnectionString()
        {
            return _connectionString;
        }
        // public void TestConnection()
        // {
        //     using (SqlConnection conn = new SqlConnection(_connectionString))
        //     {
        //         conn.Open();
        //         Console.WriteLine("Connected to Azure SQL Database!");
        //     }
        // }    
    }
}