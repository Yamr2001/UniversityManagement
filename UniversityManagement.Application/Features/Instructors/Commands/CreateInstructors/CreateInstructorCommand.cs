using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.CreateInstructors
{
    public class CreateInstructorCommand : ICommand<CommonResponse<int>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
