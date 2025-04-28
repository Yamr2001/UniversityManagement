using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById
{
    public class GetEnrollmentByIdQuery : IQuery<CommonResponse<GetEnrollmentByIdVm>>
    {
        public int Id { get; set; }
    }
}
