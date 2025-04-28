using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Courses.Commands.DeleteCourses
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteCourseCommandHandler> _logger;

        public DeleteCourseCommandHandler(IUnitOfWork uow, ILogger<DeleteCourseCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseToDelete = await _uow.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);
                await _uow.DepartmentRepository.DeleteAsync(CourseToDelete, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Course Deleted successfully",
                    Data = CourseToDelete.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error Deleted Course");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Deleted the Course",
                    Data = 0,
                };
                throw;
            }
        }
    }
}