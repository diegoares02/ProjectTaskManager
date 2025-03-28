using Microsoft.Data.SqlClient;

namespace TaskManager.Core.Application.Interfaces
{
    public interface IDatabaseConnection
    {
        SqlConnection GetConnection();
    }
}
