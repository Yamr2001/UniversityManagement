using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;
namespace UniversityManagement.Domain.Entities.Courses
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<QueryResult<Course>> GetPagedCourseList(CourseQuery courseQuery, CancellationToken cancellationToken);
    }
}
