using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Commands.DeleteEnrollments
{
    public class DeleteEnrollmentCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
    }
}
