using Microsoft.AspNetCore.Http;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Infrastructure.Repositories.Courses;
using UniversityManagement.Infrastructure.Repositories.Departments;
using UniversityManagement.Infrastructure.Repositories.Enrollments;
using UniversityManagement.Infrastructure.Repositories.Instructors;
using UniversityManagement.Infrastructure.Repositories.Students;
using UniversityManagement.Infrastructure.Repositories.Users;

namespace UniversityManagement.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public UnitOfWork(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }


        private IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository =>
            _studentRepository ??= new StudentRepository(_context, _contextAccessor);

        private IEnrollmentRepository _enrollmentRepository;
        public IEnrollmentRepository EnrollmentRepository =>
            _enrollmentRepository ??= new EnrollmentRepository(_context, _contextAccessor);


        private ICourseRepository _courseRepository;
        public ICourseRepository CourseRepository =>
            _courseRepository ??= new CourseRepository(_context, _contextAccessor);

        private IDepartmentRepository _departmentRepository;
        public IDepartmentRepository DepartmentRepository =>
            _departmentRepository ??= new DepartmentRepository(_context, _contextAccessor);

        private IInstructorRepository _instructorRepository;
        public IInstructorRepository InstructorRepository =>
            _instructorRepository ??= new InstructorRepository(_context, _contextAccessor);
        private IUserRepository _userRepository;
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context, _contextAccessor);

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CompleteWithAudit(string adminUserId)
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
