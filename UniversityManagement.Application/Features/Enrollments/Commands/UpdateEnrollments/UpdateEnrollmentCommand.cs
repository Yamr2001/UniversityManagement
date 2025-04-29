using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Enums;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments
{
    public class UpdateEnrollmentCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
        public Grade Grade { get; set; }
    }
}
