using FluentValidation;

namespace UniversityManagement.Application.Features.Enrollments.Commands.CreateEnrollments
{
    public class CreateEnrollmentValidator : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("StudentId must be a positive number.");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be a positive number.");

            RuleFor(x => x.Grade)
                .IsInEnum().WithMessage("Grade must be a valid value.");

            RuleFor(x => x.EnrollmentDate)
                .NotEmpty().WithMessage("Enrollment date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Enrollment date cannot be in the future.");
        }
    }
}
