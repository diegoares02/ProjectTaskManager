using MediatR;

namespace TaskManager.Core.Application.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest
    {
        public int TaskId { get; set; }

        public DeleteTaskCommand(int taskId)
        {
            TaskId = taskId;
        }
    }
}
