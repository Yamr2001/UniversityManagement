using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.CreateCourses
{
    public class CreateCourseCommand : ICommand<CommonResponse<int>>
    {
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
