using System.ComponentModel.DataAnnotations;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Shared.Comman;
using UniversityManagement.Shared.Enums;

namespace UniversityManagement.Domain.Entities.Enrollments
{
    public class Enrollment : AuditEntity<int>
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Grade? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
