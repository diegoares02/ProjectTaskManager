using MediatR;

namespace TaskManager.Core.Application.Tasks.Queries
{
    public class GetTaskByProjectQuery : IRequest<Domain.Entities.Task>
    {
        public int ProjectId { get; set; }

        public GetTaskByProjectQuery(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
