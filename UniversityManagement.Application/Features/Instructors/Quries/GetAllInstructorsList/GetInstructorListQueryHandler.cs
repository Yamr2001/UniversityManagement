using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Instructors.Quries.GetAllInstructorsList
{
    public class GetInstructorListQueryHandler : IQueryHandler<GetInstructorListQuery, QueryResultResource<GetInstructorListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInstructorListQueryHandler> _logger;

        public GetInstructorListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetInstructorListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetInstructorListVm>> Handle(GetInstructorListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var InstractorQuery = _mapper.Map<InstructorQuery>(request);
                var queryResult = await _uow.InstructorRepository.GetPagedInstructorList(InstractorQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetInstructorListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Instractor");
                return new();
            }
        }

    }
}
