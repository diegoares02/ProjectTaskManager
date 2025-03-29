using MediatR;

namespace TaskManager.Core.Application.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<Domain.Entities.Task>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int? AssignedTo { get; set; }
    }
}
