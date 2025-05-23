﻿using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Domain.Entities.Students
{
    public interface IStudentRepository : IGenericRepository<Student, int>
    {
        Task<QueryResult<Student>> GetPagedStudentList(StudentQuery studentQuery, CancellationToken cancellationToken);
        Task<Student?> GetByIdWithInclude(int Id, CancellationToken cancellationToken);
    }
}
