using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.DeleteInstructors
{
    public class DeleteInstructorCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
    }
}
