using UniversityManagement.Shared.Enums;

namespace UniversityManagement.Application.Features.Students.Quries.GetStudentById
{
    public class EnrollmentResponseVm
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Grade? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
