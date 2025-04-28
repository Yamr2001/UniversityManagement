using System.ComponentModel.DataAnnotations;
using UniversityManagement.Domain.Entities.OfficeAssignments;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Domain.Entities.Instructors
{
    public class Instructor : AuditEntity<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; } = null!;
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
