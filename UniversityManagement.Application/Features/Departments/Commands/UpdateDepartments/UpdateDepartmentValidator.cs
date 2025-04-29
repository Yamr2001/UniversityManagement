using FluentValidation;

namespace UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Department name is required.")
              .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

            RuleFor(x => x.Budget)
                .GreaterThan(0).WithMessage("Budget must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the future.");

            RuleFor(x => x.AdministratorId)
                .GreaterThan(0).When(x => x.AdministratorId.HasValue)
                .WithMessage("AdministratorId must be a valid positive number.");

        }
    }
}