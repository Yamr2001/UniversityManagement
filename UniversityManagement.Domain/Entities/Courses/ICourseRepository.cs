﻿using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;
namespace UniversityManagement.Domain.Entities.Courses
{
    public interface ICourseRepository : IGenericRepository<Course, int>
    {
        Task<QueryResult<Course>> GetPagedCourseList(CourseQuery courseQuery, CancellationToken cancellationToken);
        Task<Course?> GetByIdWithInclude(int Id, CancellationToken cancellationToken);
    }
}
