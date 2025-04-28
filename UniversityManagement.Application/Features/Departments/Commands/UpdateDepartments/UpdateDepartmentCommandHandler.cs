using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments
{
    public class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;

        public UpdateDepartmentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateDepartmentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Department = await _uow.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);
                _mapper.Map(request, Department);
                await _uow.DepartmentRepository.UpdateAsync(Department, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Department updated successfully",
                    Data = Department.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error updating Department");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the Department",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
