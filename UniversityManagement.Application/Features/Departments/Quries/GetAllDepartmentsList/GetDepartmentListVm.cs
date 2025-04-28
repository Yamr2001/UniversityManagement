namespace UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList
{
    public class GetDepartmentListVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
    }
}
