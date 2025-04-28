using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.DeleteDepartments
{
    public class DeleteDepartmentsCommandHandler : ICommandHandler<DeleteDepartmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteDepartmentsCommandHandler> _logger;

        public DeleteDepartmentsCommandHandler(IUnitOfWork uow, ILogger<DeleteDepartmentsCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var DepartmentToDelete = await _uow.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);
                await _uow.DepartmentRepository.DeleteAsync(DepartmentToDelete, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Department Deleted successfully",
                    Data = DepartmentToDelete.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error Deleted Department");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Deleted the Department",
                    Data = 0,
                };
                throw;
            }
        }
    }
}