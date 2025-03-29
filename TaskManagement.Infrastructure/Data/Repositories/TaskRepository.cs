using System.Data;
using Microsoft.Data.SqlClient;
using TaskManager.Core.Application.Interfaces;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public TaskRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<TaskManager.Core.Domain.Entities.Task> GetByIdAsync(int taskId)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("ReadTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@task_id", taskId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TaskManager.Core.Domain.Entities.Task
                            {
                                TaskId = (int)reader["task_id"],
                                ProjectId = (int)reader["project_id"],
                                Title = (string)reader["title"],
                                Description = (string)reader["description"],
                                DueDate = reader["due_date"] == DBNull.Value ? null : (DateTime?)reader["due_date"],
                                Priority = (string)reader["priority"],
                                Status = (string)reader["status"],
                                AssignedTo = reader["assigned_to"] == DBNull.Value ? null : (int?)reader["assigned_to"],
                                CreatedAt = (DateTime)reader["created_at"],
                                UpdatedAt = (DateTime)reader["updated_at"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<TaskManager.Core.Domain.Entities.Task>> GetTasksByProjectIdAsync(int projectId)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("GetProjectTasks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@project_id", projectId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<TaskManager.Core.Domain.Entities.Task> tasks = new List<TaskManager.Core.Domain.Entities.Task>();
                        while (await reader.ReadAsync())
                        {
                            tasks.Add(new TaskManager.Core.Domain.Entities.Task
                            {
                                TaskId = (int)reader["task_id"],
                                ProjectId = (int)reader["project_id"],
                                Title = (string)reader["title"],
                                Description = (string)reader["description"],
                                DueDate = reader["due_date"] == DBNull.Value ? null : (DateTime?)reader["due_date"],
                                Priority = (string)reader["priority"],
                                Status = (string)reader["status"],
                                AssignedTo = reader["assigned_to"] == DBNull.Value ? null : (int?)reader["assigned_to"],
                                CreatedAt = (DateTime)reader["created_at"],
                                UpdatedAt = (DateTime)reader["updated_at"]
                            });
                        }
                        return tasks;
                    }
                }
            }
        }

        public async Task<IEnumerable<TaskManager.Core.Domain.Entities.Task>> GetTasksByUserIdAsync(int userId)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("GetUserTasks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<TaskManager.Core.Domain.Entities.Task> tasks = new List<TaskManager.Core.Domain.Entities.Task>();
                        while (await reader.ReadAsync())
                        {
                            tasks.Add(new TaskManager.Core.Domain.Entities.Task
                            {
                                TaskId = (int)reader["task_id"],
                                ProjectId = (int)reader["project_id"],
                                Title = (string)reader["title"],
                                Description = (string)reader["description"],
                                DueDate = reader["due_date"] == DBNull.Value ? null : (DateTime?)reader["due_date"],
                                Priority = (string)reader["priority"],
                                Status = (string)reader["status"],
                                AssignedTo = reader["assigned_to"] == DBNull.Value ? null : (int?)reader["assigned_to"],
                                CreatedAt = (DateTime)reader["created_at"],
                                UpdatedAt = (DateTime)reader["updated_at"]
                            });
                        }
                        return tasks;
                    }
                }
            }
        }

        public async Task AddAsync(TaskManager.Core.Domain.Entities.Task task)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UpsertTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@project_id", task.ProjectId);
                    command.Parameters.AddWithValue("@title", task.Title);
                    command.Parameters.AddWithValue("@description", task.Description);
                    command.Parameters.AddWithValue("@due_date", task.DueDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@priority", task.Priority.ToString());
                    command.Parameters.AddWithValue("@status", task.Status.ToString());
                    command.Parameters.AddWithValue("@assigned_to", task.AssignedTo ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(TaskManager.Core.Domain.Entities.Task task)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UpsertTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@task_id", task.TaskId);
                    command.Parameters.AddWithValue("@project_id", task.ProjectId);
                    command.Parameters.AddWithValue("@title", task.Title);
                    command.Parameters.AddWithValue("@description", task.Description);
                    command.Parameters.AddWithValue("@due_date", task.DueDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@priority", task.Priority.ToString());
                    command.Parameters.AddWithValue("@status", task.Status.ToString());
                    command.Parameters.AddWithValue("@assigned_to", task.AssignedTo ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int taskId)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DeleteTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@task_id", taskId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AssignTaskToUserAsync(int taskId, int userId)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UpsertTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@task_id", taskId);
                    command.Parameters.AddWithValue("@assigned_to", userId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
