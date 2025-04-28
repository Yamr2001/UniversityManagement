using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.CreateDepartments
{
    public class CreateDepartmentCommand : ICommand<CommonResponse<int>>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
    }
}
