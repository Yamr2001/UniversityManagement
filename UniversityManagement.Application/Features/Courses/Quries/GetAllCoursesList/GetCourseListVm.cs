namespace UniversityManagement.Application.Features.Courses.Quries.GetAllCoursesList
{
    public class GetCourseListVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
