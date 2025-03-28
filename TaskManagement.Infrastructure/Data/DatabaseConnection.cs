using Microsoft.Data.SqlClient;
using TaskManager.Core.Application.Interfaces;

namespace TaskManagement.Infrastructure.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string? _connectionString;
        public DatabaseConnection(string? connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection GetConnection()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new System.Exception("Connection string is not set");
            }
            return new SqlConnection(_connectionString);
        }
    }
}
