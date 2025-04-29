namespace UniversityManagement.Application.Features.Courses.Quries.GetCoursesById
{
    public class GetCourseByIdVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentResponseVm Department { get; set; }
    }
}
