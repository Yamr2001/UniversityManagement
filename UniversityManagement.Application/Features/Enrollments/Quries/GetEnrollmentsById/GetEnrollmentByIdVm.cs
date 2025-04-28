using UniversityManagement.Shared.Enums;

namespace UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById
{
    public class GetEnrollmentByIdVm
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Grade Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
