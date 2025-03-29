using MediatR;

namespace TaskManager.Core.Application.Tasks.Commands
{
    public class AssignTaskCommand : IRequest
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }

        public AssignTaskCommand(int taskId, int userId)
        {
            TaskId = taskId;
            UserId = userId;
        }
    }
}
