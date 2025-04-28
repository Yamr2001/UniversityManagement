using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Instructors
{
    public class InstructorRepository : GenericRepositoryBase<Instructor>, IInstructorRepository
    {
        private readonly ApplicationDbContext _context;
        public InstructorRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context;
        }

        public async Task<QueryResult<Instructor>> GetPagedInstructorList(InstructorQuery instructorQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<Instructor>();
            var query = _context.Instructors.AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<Instructor, object>>>
            {
                ["id"] = x => x.Id,
                ["firstname"] = x => x.FirstName,
                ["lastname"] = x => x.LastName,
                ["hiredate"] = x => x.HireDate,
                ["location"] = x => x.OfficeAssignment.Location
            };

            var columnsFilter = new Dictionary<string, Expression<Func<Instructor, bool>>>
            {
                ["location"] = x => x.OfficeAssignment.Location.Contains(instructorQuery.Filter ?? string.Empty)
            };

            var orConditions = new List<Expression<Func<Instructor, bool>>>();
            string trimFilter = instructorQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(instructorQuery.Filter))
            {
                orConditions.Add(x => x.FirstName.Contains(trimFilter));
                orConditions.Add(x => x.LastName.Contains(trimFilter));
                orConditions.Add(x => x.OfficeAssignment.Location.Contains(trimFilter));
            }

            query = query.ApplyFiltering(instructorQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(instructorQuery, columnsOrder);
            query = query.ApplyPaging(instructorQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }

    }
}
