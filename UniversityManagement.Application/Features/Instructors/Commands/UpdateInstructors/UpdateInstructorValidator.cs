using FluentValidation;

namespace UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors
{
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorCommand>
    {
        public UpdateInstructorValidator()
        {

            RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("First name is required.")
             .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Hire date cannot be in the future.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters.");
        }
    }
}