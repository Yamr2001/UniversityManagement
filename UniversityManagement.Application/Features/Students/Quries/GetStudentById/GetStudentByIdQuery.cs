using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Quries.GetStudentById
{
    public class GetStudentByIdQuery : IQuery<CommonResponse<GetStudentsByIdVm>>
    {
        public int Id { get; set; }
    }
}
