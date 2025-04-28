using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Shared.Comman;
using UniversityManagement.Shared.Interfaces;

namespace UniversityManagement.Shared.Infrastructure
{
    public class GenericRepositoryBase<T> : IGenericRepository<T> where T : BaseEntity<int>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly DbSet<T> _entities;
        private readonly DbContext _context;

        public GenericRepositoryBase(DbContext context, IHttpContextAccessor accessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        protected string CurrentUser => _accessor.HttpContext?.User.Identity?.Name;

        protected string UserId => _accessor.HttpContext != null ?
            (_accessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "NameIdentifier")?.Value) : string.Empty;

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            if (typeof(IEntityBase).IsAssignableFrom(typeof(T)))
            {
                var audit = (IEntityBase)entity;
                audit.SetCreator(UserId, DateTime.UtcNow);
            }
            await _entities.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                if (typeof(IEntityBase).IsAssignableFrom(typeof(T)))
                {
                    var audit = (IEntityBase)entity;
                    audit.SetCreator(UserId, DateTime.UtcNow);
                }
            }
            await _entities.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task<bool> CheckIfEntityExistsAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken)
        {
            return await _entities.AnyAsync(expr, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                var softDelete = (IDeleteEntity)entity;
                softDelete.IsDeleted = true;

                if (typeof(IEntityBase).IsAssignableFrom(typeof(T)))
                {
                    var audit = (IEntityBase)entity;
                    audit.SetUpdater(UserId, DateTime.UtcNow);
                }

                _entities.Update(entity);
            }
            else
            {
                _entities.Remove(entity);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var entity in entities)
                {
                    var softDelete = (IDeleteEntity)entity;
                    softDelete.IsDeleted = true;

                    if (typeof(IEntityBase).IsAssignableFrom(typeof(T)))
                    {
                        var audit = (IEntityBase)entity;
                        audit.SetUpdater(UserId, DateTime.UtcNow);
                    }
                }
                _entities.UpdateRange(entities);
            }
            else
            {
                _entities.RemoveRange(entities);
            }
            await Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _entities.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (typeof(IEntityBase).IsAssignableFrom(typeof(T)))
            {
                var audit = (IEntityBase)entity;
                audit.SetUpdater(UserId, DateTime.UtcNow);
            }
            _entities.Update(entity);
            await Task.CompletedTask;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _entities.CountAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public IQueryable<T> Query()
        {
            return _entities.AsQueryable();
        }
    }
}