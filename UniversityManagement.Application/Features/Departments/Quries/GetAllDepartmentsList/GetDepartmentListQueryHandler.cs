using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList
{
    public class GetDepartmentListQueryHandler : IQueryHandler<GetDepartmentListQuery, QueryResultResource<GetDepartmentListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDepartmentListQueryHandler> _logger;

        public GetDepartmentListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetDepartmentListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetDepartmentListVm>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentQuery = _mapper.Map<DepartmentQuery>(request);
                var queryResult = await _uow.DepartmentRepository.GetPagedDepartmentList(StudentQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetDepartmentListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student");
                return new();
            }
        }

    }
}
