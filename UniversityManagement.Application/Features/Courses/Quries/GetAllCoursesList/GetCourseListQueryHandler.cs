using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Courses.Quries.GetAllCoursesList
{
    public class GetCourseListQueryHandler : IQueryHandler<GetCourseListQuery, QueryResultResource<GetCourseListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourseListQueryHandler> _logger;

        public GetCourseListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetCourseListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetCourseListVm>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentQuery = _mapper.Map<CourseQuery>(request);
                var queryResult = await _uow.CourseRepository.GetPagedCourseList(StudentQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetCourseListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Courses");
                return new();
            }
        }

    }
}
