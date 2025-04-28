using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList
{
    public class GetDepartmentListQuery : IQuery<QueryResultResource<GetDepartmentListVm>>, IQueryObject
    {
        public string SortBy
        {
            get; set;
        } = string.Empty;
        public bool IsSortAscending
        {
            get; set;
        }
        public int Page
        {
            get; set;
        }
        public int PageSize
        {
            get; set;
        }
        public int Id
        {
            get; set;
        }
        public string Filter
        {
            get; set;
        } = string.Empty;
    }
}
