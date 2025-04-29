using FluentValidation;

namespace UniversityManagement.Application.Features.Courses.Commands.UpdateCourses
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Title is required.")
    .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Credits)
                .GreaterThan(0).WithMessage("Credits must be greater than zero.")
                .LessThanOrEqualTo(10).WithMessage("Credits must not exceed 10.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("DepartmentId must be valid (greater than 0).");

        }
    }
}