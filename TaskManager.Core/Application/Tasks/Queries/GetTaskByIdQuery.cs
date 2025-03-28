using MediatR;

namespace TaskManager.Core.Application.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<Domain.Entities.Task>
    {
        public int TaskId { get; set; }

        public GetTaskByIdQuery(int taskId)
        {
            TaskId = taskId;
        }
    }
}
