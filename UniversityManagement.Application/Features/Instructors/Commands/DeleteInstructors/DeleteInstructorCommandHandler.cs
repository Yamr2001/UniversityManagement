using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.DeleteInstructors
{
    public class DeleteInstructorCommandHandler : ICommandHandler<DeleteInstructorCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteInstructorCommandHandler> _logger;

        public DeleteInstructorCommandHandler(IUnitOfWork uow, ILogger<DeleteInstructorCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InstructorToDelete = await _uow.InstructorRepository.GetByIdAsync(request.Id, cancellationToken);
                await _uow.InstructorRepository.DeleteAsync(InstructorToDelete, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Instructor Deleted successfully",
                    Data = InstructorToDelete.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error Deleted Instructor");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Deleted the Instructor",
                    Data = 0,
                };
                throw;
            }
        }
    }
}