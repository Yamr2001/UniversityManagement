using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.CreateCourses
{
    public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCourseCommandHandler> _logger;

        public CreateCourseCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateCourseCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseToCreate = _mapper.Map<Course>(request);
                await _uow.CourseRepository.AddAsync(CourseToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Course created successfully",
                    Data = CourseToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating Course");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the Course",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
