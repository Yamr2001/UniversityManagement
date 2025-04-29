using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Courses
{
    public class CourseRepository : GenericRepositoryBase<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context;
        }


        public async Task<Course?> GetByIdWithInclude(int Id, CancellationToken cancellationToken)
        {
            return await _context.Courses
                 .IgnoreQueryFilters()
                  .Where(c => !c.IsDeleted)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
        public async Task<QueryResult<Course>> GetPagedCourseList(CourseQuery courseQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<Course>();
            var query = _context.Courses
                  .IgnoreQueryFilters()
                  .Where(c => !c.IsDeleted)
                .Include(c => c.Department)
                                        .Include(c => c.Enrollments)
                                        .AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<Course, object>>>
            {
                ["id"] = x => x.Id,
                ["title"] = x => x.Title,
                ["credits"] = x => x.Credits,
                ["department"] = x => x.Department.Name
            };

            var columnsFilter = new Dictionary<string, Expression<Func<Course, bool>>>
            {
                ["title"] = x => x.Title.Contains(courseQuery.Filter ?? string.Empty),
                ["department"] = x => x.Department.Name.Contains(courseQuery.Filter ?? string.Empty),
                ["credits"] = x => x.Credits.ToString().Contains(courseQuery.Filter ?? string.Empty)
            };

            var orConditions = new List<Expression<Func<Course, bool>>>();
            string trimFilter = courseQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(courseQuery.Filter))
            {
                orConditions.Add(x => x.Title.Contains(trimFilter));
                orConditions.Add(x => x.Department.Name.Contains(trimFilter));
                orConditions.Add(x => x.Credits.ToString().Contains(trimFilter));
            }

            query = query.ApplyFiltering(courseQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(courseQuery, columnsOrder);

            query = query.ApplyPaging(courseQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }

    }
}
