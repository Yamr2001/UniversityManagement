using System.ComponentModel.DataAnnotations;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Domain.Entities.Departments
{
    public class Department : AuditEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? AdministratorId { get; set; }
        public Instructor Administrator { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = [];
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
