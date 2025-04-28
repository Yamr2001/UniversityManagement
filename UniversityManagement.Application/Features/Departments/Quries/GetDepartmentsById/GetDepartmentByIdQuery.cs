using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById
{
    public class GetDepartmentByIdQuery : IQuery<CommonResponse<GetDepartmentByIdVm>>
    {
        public int Id { get; set; }
    }
}
