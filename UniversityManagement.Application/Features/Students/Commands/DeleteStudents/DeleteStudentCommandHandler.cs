using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Commands.DeleteStudents
{
    public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(IUnitOfWork uow, ILogger<DeleteStudentCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentToDelete = await _uow.StudentRepository.GetByIdAsync(request.Id, cancellationToken);
                await _uow.StudentRepository.DeleteAsync(StudentToDelete, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Student Deleted successfully",
                    Data = StudentToDelete.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error Deleted student");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Deleted the student",
                    Data = 0,
                };
                throw;
            }
        }
    }
}