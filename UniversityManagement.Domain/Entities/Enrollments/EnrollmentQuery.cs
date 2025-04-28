using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Domain.Entities.Enrollments
{
    public class EnrollmentQuery : IQueryObject
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
        public int? Id
        {
            get; set;
        }
        public string Filter
        {
            get; set;
        } = string.Empty;
    }
}
