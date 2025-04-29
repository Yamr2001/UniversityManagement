namespace UniversityManagement.Application.Features.Courses.Quries.GetCoursesById
{
    public class DepartmentResponseVm
    {
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
    }
}
