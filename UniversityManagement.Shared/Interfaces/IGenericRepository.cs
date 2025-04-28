using System.Linq.Expressions;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Shared.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<bool> CheckIfEntityExistsAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task DeleteRangeAsync(IEnumerable<T> entites, CancellationToken cancellationToken);
    }
}
