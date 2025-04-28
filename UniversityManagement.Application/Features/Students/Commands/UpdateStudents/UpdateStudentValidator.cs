using FluentValidation;

namespace UniversityManagement.Application.Features.Students.Commands.UpdateStudents
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentsCommand>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$")
                .WithMessage("Invalid phone number format");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of birth is required")
                .LessThan(DateTime.Today)
                .WithMessage("Date of birth must be in the past");

        }
    }
}