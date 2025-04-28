using System.ComponentModel.DataAnnotations;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Domain.Entities.Courses
{
    public class Course : AuditEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
