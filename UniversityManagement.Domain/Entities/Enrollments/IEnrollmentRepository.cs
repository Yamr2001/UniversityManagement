﻿using UniversityManagement.Shared.Interfaces;
using UniversityManagement.Shared.Resourses;
namespace UniversityManagement.Domain.Entities.Enrollments
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment, int>
    {
        Task<QueryResult<Enrollment>> GetPagedEnrollmentList(EnrollmentQuery enrollmentQuery, CancellationToken cancellationToken);
    }
}
