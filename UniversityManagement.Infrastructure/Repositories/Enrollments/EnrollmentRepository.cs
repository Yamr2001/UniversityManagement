using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Enrollments
{
    public class EnrollmentRepository : GenericRepositoryBase<Enrollment, int>, IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context;
        }

        public async Task<QueryResult<Enrollment>> GetPagedEnrollmentList(EnrollmentQuery enrollmentQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<Enrollment>();
            var query = _context.Enrollments
                 .IgnoreQueryFilters()
                  .Where(c => !c.IsDeleted)
                .Include(e => e.Student)
                 .Include(e => e.Course)
                 .AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<Enrollment, object>>>
            {
                ["id"] = x => x.Id,
                ["studentname"] = x => x.Student.Name,
                ["coursename"] = x => x.Course.Title,
                ["grade"] = x => x.Grade,
                ["enrollmentdate"] = x => x.EnrollmentDate
            };

            var columnsFilter = new Dictionary<string, Expression<Func<Enrollment, bool>>>
            {
                ["studentname"] = x => x.Student.Name.Contains(enrollmentQuery.Filter ?? string.Empty),
                ["coursename"] = x => x.Course.Title.Contains(enrollmentQuery.Filter ?? string.Empty),
                ["grade"] = x => x.Grade.ToString().Contains(enrollmentQuery.Filter ?? string.Empty)
            };

            var orConditions = new List<Expression<Func<Enrollment, bool>>>();
            string trimFilter = enrollmentQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(enrollmentQuery.Filter))
            {
                orConditions.Add(x => x.Student.Name.Contains(trimFilter));
                orConditions.Add(x => x.Course.Title.Contains(trimFilter));
                orConditions.Add(x => x.Grade.ToString().Contains(trimFilter));
            }

            query = query.ApplyFiltering(enrollmentQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(enrollmentQuery, columnsOrder);

            query = query.ApplyPaging(enrollmentQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }



    }
}
