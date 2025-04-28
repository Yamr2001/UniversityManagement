using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments
{
    public class UpdateDepartmentCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
    }
}
