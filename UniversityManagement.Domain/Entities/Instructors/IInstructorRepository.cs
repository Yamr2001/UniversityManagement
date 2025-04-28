using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Domain.Entities.Instructors
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task<QueryResult<Instructor>> GetPagedInstructorList(InstructorQuery instructorQuery, CancellationToken cancellationToken);
    }
}
