using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Commands.DeleteStudents
{
    public class DeleteStudentCommand : ICommand<CommonResponse<int>>
    {
        public int Id { get; set; }
    }
}
