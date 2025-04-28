using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.UpdateCourses
{
    public class UpdateCourseCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
