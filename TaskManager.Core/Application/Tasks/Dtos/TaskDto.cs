using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Tasks.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public Domain.Entities.TaskStatus Status { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
