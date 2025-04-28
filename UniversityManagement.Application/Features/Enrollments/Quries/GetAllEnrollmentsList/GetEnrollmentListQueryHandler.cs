using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Enrollments.Quries.GetAllEnrollmentsList
{
    public class GetEnrollmentListQueryHandler : IQueryHandler<GetEnrollmentListQuery, QueryResultResource<GetEnrollmentListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEnrollmentListQueryHandler> _logger;

        public GetEnrollmentListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetEnrollmentListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetEnrollmentListVm>> Handle(GetEnrollmentListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var EnrollmentQuery = _mapper.Map<EnrollmentQuery>(request);
                var queryResult = await _uow.EnrollmentRepository.GetPagedEnrollmentList(EnrollmentQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetEnrollmentListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Enrollment");
                return new();
            }
        }

    }
}
