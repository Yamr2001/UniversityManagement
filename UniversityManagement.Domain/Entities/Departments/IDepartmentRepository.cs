using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;
namespace UniversityManagement.Domain.Entities.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department, int>
    {
        Task<QueryResult<Department>> GetPagedDepartmentList(DepartmentQuery departmentQuery, CancellationToken cancellationToken);
    }
}
