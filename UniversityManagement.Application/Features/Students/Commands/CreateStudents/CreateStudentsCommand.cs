using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Enums;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Commands.CreateStudents
{
    public class CreateStudentsCommand : ICommand<CommonResponse<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
