using MediatR;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManager.Core.Application.Tasks.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Domain.Entities.Task>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Domain.Entities.Task> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = request.Status,
                AssignedTo = request.AssignedTo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);

            return task;
        }
    }
}
