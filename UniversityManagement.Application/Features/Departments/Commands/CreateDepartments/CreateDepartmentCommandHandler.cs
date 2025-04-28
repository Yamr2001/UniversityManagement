using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Commands.CreateDepartments
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;

        public CreateDepartmentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateDepartmentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var DepartmentToCreate = _mapper.Map<Department>(request);
                await _uow.DepartmentRepository.AddAsync(DepartmentToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Department created successfully",
                    Data = DepartmentToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating Department");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the Department",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
