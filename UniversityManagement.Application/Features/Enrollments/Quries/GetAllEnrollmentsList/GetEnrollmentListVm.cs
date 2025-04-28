using UniversityManagement.Shared.Enums;

namespace UniversityManagement.Application.Features.Enrollments.Quries.GetAllEnrollmentsList
{
    public class GetEnrollmentListVm
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Grade Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
