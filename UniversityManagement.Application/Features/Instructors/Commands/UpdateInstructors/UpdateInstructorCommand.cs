using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors
{
    public class UpdateInstructorCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
