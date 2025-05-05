using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepositoryBase<User, Guid>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context;
        }

        public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email.Trim(), cancellationToken);
        }

        public async Task<QueryResult<User>> GetPagedUserList(UserQuery userQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<User>();
            var query = _context.Users
                 .AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["id"] = x => x.Id,
                ["email"] = x => x.Email,
            };

            var columnsFilter = new Dictionary<string, Expression<Func<User, bool>>>
            {
                ["studentname"] = x => x.Email.Contains(userQuery.Filter ?? string.Empty),
            };

            var orConditions = new List<Expression<Func<User, bool>>>();
            string trimFilter = userQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(userQuery.Filter))
            {
                orConditions.Add(x => x.Email.Contains(trimFilter));
            }

            query = query.ApplyFiltering(userQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(userQuery, columnsOrder);

            query = query.ApplyPaging(userQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }



    }
}
