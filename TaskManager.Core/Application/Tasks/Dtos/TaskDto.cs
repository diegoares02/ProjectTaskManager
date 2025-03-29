namespace TaskManager.Core.Application.Tasks.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int? AssignedTo { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
