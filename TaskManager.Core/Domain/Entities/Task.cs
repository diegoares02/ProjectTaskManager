namespace TaskManager.Core.Domain.Entities
{
    public enum TaskPriority { Low, Medium, High }
    public enum TaskStatus { ToDo, InProgress, Completed }
    public class Task
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
