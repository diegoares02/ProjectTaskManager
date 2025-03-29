using Microsoft.Data.SqlClient;
using System.Data;
using TaskManager.Core.Application.Interfaces;

namespace TaskManager.Core.Application.Tasks.Validators
{
    public interface ITaskDependencyValidator
    {
        Task<bool> UserIdExists(int? userId, CancellationToken cancellationToken);
        Task<bool> ProjectIdExists(int projectId, CancellationToken cancellationToken);
        Task<bool> TitleExists(string title, CancellationToken cancellationToken);
        Task<bool> TaskIdExists(int taskId, CancellationToken cancellationToken);
    }

    public class TaskDependencyValidator : ITaskDependencyValidator
    {
        private readonly IDatabaseConnection _databaseConnection;
        public TaskDependencyValidator(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public async Task<bool> UserIdExists(int? userId, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync(cancellationToken);
                using (SqlCommand command = new SqlCommand("CheckUserIdExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlParameter existsParam = new SqlParameter("@Exists", SqlDbType.Bit);
                    existsParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(existsParam);
                    await command.ExecuteNonQueryAsync(cancellationToken);
                    return (bool)existsParam.Value;
                }
            }
        }
        public async Task<bool> ProjectIdExists(int projectId, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync(cancellationToken);

                using (SqlCommand command = new SqlCommand("CheckProjectIdExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProjectId", projectId);

                    SqlParameter existsParam = new SqlParameter("@Exists", SqlDbType.Bit);
                    existsParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(existsParam);

                    await command.ExecuteNonQueryAsync(cancellationToken);

                    return (bool)existsParam.Value;
                }
            }
        }
        public async Task<bool> TitleExists(string title, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync(cancellationToken);
                using (SqlCommand command = new SqlCommand("CheckTaskTitleExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", title);
                    SqlParameter existsParam = new SqlParameter("@Exists", SqlDbType.Bit);
                    existsParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(existsParam);
                    await command.ExecuteNonQueryAsync(cancellationToken);
                    return !(bool)existsParam.Value;
                }
            }
        }
        public async Task<bool> TaskIdExists(int taskId, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync(cancellationToken);
                using (SqlCommand command = new SqlCommand("CheckTaskIdExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    SqlParameter existsParam = new SqlParameter("@Exists", SqlDbType.Bit);
                    existsParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(existsParam);
                    await command.ExecuteNonQueryAsync(cancellationToken);
                    return (bool)existsParam.Value;
                }
            }
        }
    }
}
