using FluentValidation;
using MediatR;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Application.Tasks.Validators;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManager.Core.Application.Tasks.Handlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Domain.Entities.Task>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskDependencyValidator _taskDependenciesValidator;

        public UpdateTaskHandler(ITaskRepository taskRepository, ITaskDependencyValidator taskDependenciesValidator)
        {
            _taskRepository = taskRepository;
            _taskDependenciesValidator = taskDependenciesValidator;
        }

        public async Task<Domain.Entities.Task> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = request.Status,
                AssignedTo = request.AssignedTo
            };

            if (!await _taskDependenciesValidator.ProjectIdExists(task.ProjectId, cancellationToken))
            {
                throw new ValidationException("Project ID is invalid.");
            }

            if (!await _taskDependenciesValidator.UserIdExists(task.AssignedTo, cancellationToken))
            {
                throw new ValidationException("User ID is invalid.");
            }

            await _taskRepository.UpdateAsync(task);

            return task;
        }
    }
}
