using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Commands.DeleteEnrollments
{
    public class DeleteEnrollmentCommandHandler : ICommandHandler<DeleteEnrollmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteEnrollmentCommandHandler> _logger;

        public DeleteEnrollmentCommandHandler(IUnitOfWork uow, ILogger<DeleteEnrollmentCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var EnrollmentToDelete = await _uow.EnrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
                await _uow.EnrollmentRepository.DeleteAsync(EnrollmentToDelete, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Enrollment Deleted successfully",
                    Data = EnrollmentToDelete.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error Deleted Enrollment");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Deleted the Enrollment",
                    Data = 0,
                };
                throw;
            }
        }
    }
}