using MediatR;

namespace TaskManager.Core.Application.Tasks.Queries
{
    public class GetUserTasksQuery : IRequest<Domain.Entities.Task>
    {
        public int UserId { get; set; }

        public GetUserTasksQuery(int userId)
        {
            UserId = userId;
        }
    }
}
