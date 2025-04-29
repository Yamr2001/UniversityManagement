using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Departments
{
    public class DepartmentRepository : GenericRepositoryBase<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<QueryResult<Department>> GetPagedDepartmentList(DepartmentQuery departmentQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<Department>();
            var query = _context.Departments.IgnoreQueryFilters().Where(c => !c.IsDeleted).Include(d => d.Administrator)
                                            .Include(d => d.Courses)
                                            .AsQueryable();

            // Define columns for ordering
            var columnsOrder = new Dictionary<string, Expression<Func<Department, object>>>
            {
                ["id"] = x => x.Id,
                ["name"] = x => x.Name,
                ["budget"] = x => x.Budget,
                ["startdate"] = x => x.StartDate,
                ["administrator"] = x => x.Administrator.FirstName + " " + x.Administrator.LastName
            };

            var columnsFilter = new Dictionary<string, Expression<Func<Department, bool>>>
            {
                ["name"] = x => x.Name.Contains(departmentQuery.Filter ?? string.Empty),
                ["administrator"] = x => x.Administrator.FirstName.Contains(departmentQuery.Filter ?? string.Empty) ||
                                         x.Administrator.LastName.Contains(departmentQuery.Filter ?? string.Empty),
                ["budget"] = x => x.Budget.ToString().Contains(departmentQuery.Filter ?? string.Empty),
                ["startdate"] = x => x.StartDate.ToString().Contains(departmentQuery.Filter ?? string.Empty)
            };

            var orConditions = new List<Expression<Func<Department, bool>>>();
            string trimFilter = departmentQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(departmentQuery.Filter))
            {
                orConditions.Add(x => x.Name.Contains(trimFilter));
                orConditions.Add(x => x.Administrator.FirstName.Contains(trimFilter));
                orConditions.Add(x => x.Administrator.LastName.Contains(trimFilter));
                orConditions.Add(x => x.Budget.ToString().Contains(trimFilter));
                orConditions.Add(x => x.StartDate.ToString().Contains(trimFilter));
            }

            query = query.ApplyFiltering(departmentQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(departmentQuery, columnsOrder);

            query = query.ApplyPaging(departmentQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }




    }
}
