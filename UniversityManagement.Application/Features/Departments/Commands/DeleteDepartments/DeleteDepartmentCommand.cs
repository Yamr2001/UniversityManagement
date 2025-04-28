using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.DeleteDepartments
{
    public class DeleteDepartmentCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
    }
}
