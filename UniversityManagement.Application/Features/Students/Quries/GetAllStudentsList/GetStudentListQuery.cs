using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Students.Quries.GetAllStudentsList
{
    public class GetStudentListQuery : IQuery<QueryResultResource<GetStudentsListVm>>, IQueryObject
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
