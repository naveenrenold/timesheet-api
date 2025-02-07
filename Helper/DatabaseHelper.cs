namespace  TimeSheet.Helper
{    
using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper()
    {
        // var config = new ConfigurationBuilder()
        //     .AddJsonFile("appsettings.json")
        //     .Build();

       // _connectionString = "Server=tcp:time-sheet.database.windows.net,1433;Initial Catalog=main;Persist Security Info=False;User ID=naveenrenold;Password=CHRisty1002@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
       _connectionString = "Server=tcp:sqlcmd -S time-sheet.database.windows.net -d main -Gt,1433;Initial Catalog=main;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
    }

    public void TestConnection()
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            Console.WriteLine("Connected to Azure SQL Database!");
        }
    }

    public void GetData()
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "SELECT TOP 2 * FROM Employee"; 
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["EmployeeId"]}, Name: {reader["Name"]}");
                }
            }
        }
    }
}
}