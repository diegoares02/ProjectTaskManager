using FluentValidation;
using MediatR;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Application.Tasks.Validators;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManager.Core.Application.Tasks.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Domain.Entities.Task>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskDependencyValidator _taskDependenciesValidator;

        public CreateTaskHandler(ITaskRepository taskRepository, ITaskDependencyValidator taskDependenciesValidator)
        {
            _taskRepository = taskRepository;
            _taskDependenciesValidator = taskDependenciesValidator;
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

            if (!await _taskDependenciesValidator.ProjectIdExists(task.ProjectId, cancellationToken))
            {
                throw new ValidationException("Project ID is invalid.");
            }

            if (!await _taskDependenciesValidator.UserIdExists(task.AssignedTo, cancellationToken))
            {
                throw new ValidationException("User ID is invalid.");
            }

            if (!await _taskDependenciesValidator.TitleExists(task.Title, cancellationToken))
            {
                throw new ValidationException("Title already exists.");
            }

            await _taskRepository.AddAsync(task);

            return task;
        }
    }
}
