using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Domain.Entities.Users;

namespace UniversityManagement.Domain.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IUserRepository UserRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IInstructorRepository InstructorRepository { get; }

        Task Complete();
        Task CompleteWithAudit(string adminUserId);
    }
}
