using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Students.Quries.GetAllStudentsList
{
    public class GetStudentListQueryHandler : IQueryHandler<GetStudentListQuery, QueryResultResource<GetStudentsListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStudentListQueryHandler> _logger;

        public GetStudentListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetStudentListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetStudentsListVm>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentQuery = _mapper.Map<StudentQuery>(request);
                var queryResult = await _uow.StudentRepository.GetPagedStudentList(StudentQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetStudentsListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student");
                return new();
            }
        }

    }
}
