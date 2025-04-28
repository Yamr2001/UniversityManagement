using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.UpdateCourses
{
    public class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCourseCommandHandler> _logger;

        public UpdateCourseCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateCourseCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Course = await _uow.CourseRepository.GetByIdAsync(request.Id, cancellationToken);
                _mapper.Map(request, Course);
                await _uow.CourseRepository.UpdateAsync(Course, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Course updated successfully",
                    Data = Course.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error updating Course");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the Course",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
