using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Quries.GetCoursesById
{
    public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CommonResponse<GetCourseByIdVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourseByIdQueryHandler> _logger;

        public GetCourseByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetCourseByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<GetCourseByIdVm>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _uow.CourseRepository.GetByIdAsync(request.Id, cancellationToken);
                if (student == null)
                {
                    return new CommonResponse<GetCourseByIdVm>
                    {
                        Success = false,
                        Message = "Course not found",
                        Data = null
                    };
                }

                var mappedStudent = _mapper.Map<GetCourseByIdVm>(student);

                return new CommonResponse<GetCourseByIdVm>
                {
                    Success = true,
                    Message = "Course retrieved successfully",
                    Data = mappedStudent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Course");
                return new CommonResponse<GetCourseByIdVm>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the Course",
                    Data = null
                };
            }
        }

    }
}
