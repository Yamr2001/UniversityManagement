using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById
{
    public class GetInstructorByIdQuery : IQuery<CommonResponse<GetInstructorByIdVm>>
    {
        public int Id { get; set; }
    }
}
