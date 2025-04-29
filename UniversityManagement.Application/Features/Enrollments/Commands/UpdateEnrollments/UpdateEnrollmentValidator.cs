using FluentValidation;

namespace UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments
{
    public class UpdateEnrollmentValidator : AbstractValidator<UpdateEnrollmentCommand>
    {
        public UpdateEnrollmentValidator()
        {
            RuleFor(x => x.Grade)
              .IsInEnum().WithMessage("Grade must be a valid value.");

        }
    }
}