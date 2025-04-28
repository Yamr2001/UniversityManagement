using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Quries.GetCoursesById
{
    public class GetCourseByIdQuery : IQuery<CommonResponse<GetCourseByIdVm>>
    {
        public int Id { get; set; }
    }
}
