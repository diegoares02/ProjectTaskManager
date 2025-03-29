using FluentValidation;
using MediatR;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Application.Tasks.Validators;
using TaskManager.Core.Domain.Interfaces;

namespace TaskManager.Core.Application.Tasks.Handlers
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskDependencyValidator _taskDependenciesValidator;

        public DeleteTaskHandler(ITaskRepository taskRepository, ITaskDependencyValidator taskDependenciesValidator)
        {
            _taskRepository = taskRepository;
            _taskDependenciesValidator = taskDependenciesValidator;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (!await _taskDependenciesValidator.TaskIdExists(request.TaskId, cancellationToken))
            {
                throw new ValidationException("Task ID is invalid.");
            }
            await _taskRepository.DeleteAsync(request.TaskId);
        }
    }
}
