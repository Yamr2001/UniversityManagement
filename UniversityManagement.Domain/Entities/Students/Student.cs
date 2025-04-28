using System.ComponentModel.DataAnnotations;
using UniversityManagement.Domain.Entities.Addresses;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Shared.Comman;
using UniversityManagement.Shared.Enums;

namespace UniversityManagement.Domain.Entities.Students
{
    public class Student : AuditEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; }
        public Address Address { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
