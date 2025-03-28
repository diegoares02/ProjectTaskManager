namespace TaskManager.Core.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Entities.Task> GetByIdAsync(int taskId);
        Task<IEnumerable<Entities.Task>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<Entities.Task>> GetTasksByUserIdAsync(int userId);
        Task AddAsync(Entities.Task task);
        Task UpdateAsync(Entities.Task task);
        Task DeleteAsync(int taskId);
        Task AssignTaskToUserAsync(int taskId, int userId);
    }
}
