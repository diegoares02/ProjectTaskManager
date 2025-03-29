using FluentValidation;
using TaskManager.Core.Application.Tasks.Commands;

namespace TaskManager.Core.Application.Tasks.Validators
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(task => task.Title)
               .NotEmpty().WithMessage("Title is required.")
               .MaximumLength(255).WithMessage("Title must not exceed 255 characters.");

            RuleFor(task => task.ProjectId)
                .NotEmpty().WithMessage("Project ID is required.")
                .GreaterThan(0).WithMessage("Project ID must be greater than 0.");

            RuleFor(task => task.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(task => task.Priority)
                .NotEmpty().WithMessage("Priority is required.")
                .Must(IsValidPriority).WithMessage("Priority must be 'Low', 'Medium', or 'High'.");

            RuleFor(task => task.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(IsValidStatus).WithMessage("Status must be 'To Do', 'In Progress', or 'Completed'.");

            RuleFor(task => task.DueDate)
                .Must(IsAValidDate).WithMessage("Due date must be a valid date.");

            RuleFor(task => task.AssignedTo)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("Assigned to must be greater than 0.");
        }

        private bool IsAValidDate(DateTime? dueDate)
        {
            return dueDate == null || dueDate >= DateTime.Now;
        }

        private bool IsValidPriority(string priority)
        {
            return priority == "Low" || priority == "Medium" || priority == "High";
        }

        private bool IsValidStatus(string status)
        {
            return status == "To Do" || status == "In Progress" || status == "Completed";
        }
    }
}
