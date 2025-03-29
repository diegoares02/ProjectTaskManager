using TaskManager.Core.Application.Tasks.Dtos;

namespace TaskManager.Core.Application.Tasks.Mappers
{
    public class TaskMapper
    {
        public static TaskDto MapToDto(Domain.Entities.Task task)
        {
            if (task is null)
            {
                return null;
            }

            return new TaskDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate?.ToString("yyyy-MM-dd") ?? "",
                Priority = task.Priority,
                Status = task.Status,
                AssignedTo = task.AssignedTo,
                CreatedAt = task.CreatedAt.ToString("yyyy-MM-dd") ?? "",
                UpdatedAt = task.UpdatedAt.ToString("yyyy-MM-dd") ?? ""
            };
        }

        public static Domain.Entities.Task MapToEntity(TaskDto taskDto)
        {
            if (taskDto is null)
            {
                return null;
            }

            return new Domain.Entities.Task
            {
                TaskId = taskDto.TaskId,
                ProjectId = taskDto.ProjectId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate is not null ? DateTime.Parse(taskDto.DueDate) : null,
                Priority = taskDto.Priority,
                Status = taskDto.Status,
                AssignedTo = taskDto.AssignedTo,
                CreatedAt = DateTime.Parse(taskDto.CreatedAt),
                UpdatedAt = DateTime.Parse(taskDto.UpdatedAt)
            };
        }
    }
}
