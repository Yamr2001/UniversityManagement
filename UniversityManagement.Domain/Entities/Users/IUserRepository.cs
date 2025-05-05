using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Domain.Entities.Users
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<QueryResult<User>> GetPagedUserList(UserQuery userQuery, CancellationToken cancellationToken);
    }
}
