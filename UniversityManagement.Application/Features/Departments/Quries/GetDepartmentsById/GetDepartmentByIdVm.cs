namespace UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById
{
    public class GetDepartmentByIdVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
    }
}
