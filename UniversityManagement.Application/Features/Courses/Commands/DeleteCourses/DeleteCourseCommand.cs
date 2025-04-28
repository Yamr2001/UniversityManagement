using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.DeleteCourses
{
    public class DeleteCourseCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
    }
}
