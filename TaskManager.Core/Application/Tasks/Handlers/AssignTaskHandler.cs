using FluentValidation;
using MediatR;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Application.Tasks.Validators;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManager.Core.Application.Tasks.Handlers
{
    public class AssignTaskHandler : IRequestHandler<AssignTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskDependencyValidator _taskDependenciesValidator;

        public AssignTaskHandler(ITaskRepository taskRepository, ITaskDependencyValidator taskDependenciesValidator)
        {
            _taskRepository = taskRepository;
            _taskDependenciesValidator = taskDependenciesValidator;
        }

        public async Task Handle(AssignTaskCommand request, CancellationToken cancellationToken)
        {
            if (!await _taskDependenciesValidator.TaskIdExists(request.TaskId, cancellationToken))
            {
                throw new ValidationException("Task ID is invalid.");
            }

            if (!await _taskDependenciesValidator.UserIdExists(request.UserId, cancellationToken))
            {
                throw new ValidationException("User ID is invalid.");
            }

            await _taskRepository.AssignTaskToUserAsync(request.TaskId, request.UserId);
        }
    }
}
