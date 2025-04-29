using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Infrastructure.Presistence;
using UniversityManagement.Shared.Infrastructure;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Infrastructure.Repositories.Students
{
    public class StudentRepository : GenericRepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdWithInclude(int Id, CancellationToken cancellationToken)
        {
            return await _context.Students
                .IgnoreQueryFilters().Where(c => !c.IsDeleted)
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
        public async Task<QueryResult<Student>> GetPagedStudentList(StudentQuery studentQuery, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult<Student>();
            var query = _context.Students.AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<Student, object>>>
            {
                ["id"] = x => x.Id,
                ["name"] = x => x.Name,
                ["email"] = x => x.Email,
                ["phonenumber"] = x => x.PhoneNumber,
                ["dateofbirth"] = x => x.DateOfBirth,
                ["city"] = x => x.Address.City,
                ["country"] = x => x.Address.Country,
                ["street"] = x => x.Address.Street,
            };

            var columnsFilter = new Dictionary<string, Expression<Func<Student, bool>>>
            {
                ["city"] = x => x.Address.City.Contains(studentQuery.Filter ?? string.Empty),
                ["country"] = x => x.Address.Country.Contains(studentQuery.Filter ?? string.Empty),
            };

            var orConditions = new List<Expression<Func<Student, bool>>>();
            string TrimFilter = studentQuery.Filter?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(studentQuery.Filter))
            {
                orConditions.Add(x => x.Name.Contains(TrimFilter));
                orConditions.Add(x => x.Email.Contains(TrimFilter));
                orConditions.Add(x => x.PhoneNumber.Contains(TrimFilter));
                orConditions.Add(x => x.Address.City.Contains(TrimFilter));
                orConditions.Add(x => x.Address.Country.Contains(TrimFilter));
                orConditions.Add(x => x.Address.Street.Contains(TrimFilter));
            }

            query = query.ApplyFiltering(studentQuery, columnsFilter, orConditions);

            queryResult.TotalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(studentQuery, columnsOrder);
            query = query.ApplyPaging(studentQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }

    }
}
