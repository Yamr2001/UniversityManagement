using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById
{
    public class GetDepartmentByIdQueryHandler : IQueryHandler<GetDepartmentByIdQuery, CommonResponse<GetDepartmentByIdVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDepartmentByIdQueryHandler> _logger;

        public GetDepartmentByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetDepartmentByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<GetDepartmentByIdVm>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Department = await _uow.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);
                if (Department == null)
                {
                    return new CommonResponse<GetDepartmentByIdVm>
                    {
                        Success = false,
                        Message = "Department not found",
                        Data = null
                    };
                }

                var mappedStudent = _mapper.Map<GetDepartmentByIdVm>(Department);

                return new CommonResponse<GetDepartmentByIdVm>
                {
                    Success = true,
                    Message = "Department retrieved successfully",
                    Data = mappedStudent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Department");
                return new CommonResponse<GetDepartmentByIdVm>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the Department",
                    Data = null
                };
            }
        }

    }
}
