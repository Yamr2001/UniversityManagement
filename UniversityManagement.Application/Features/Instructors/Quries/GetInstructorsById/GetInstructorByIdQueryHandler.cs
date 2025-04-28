using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById
{
    public class GetInstructorByIdQueryHandler : IQueryHandler<GetInstructorByIdQuery, CommonResponse<GetInstructorByIdVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInstructorByIdQueryHandler> _logger;

        public GetInstructorByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetInstructorByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<GetInstructorByIdVm>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Instractor = await _uow.InstructorRepository.GetByIdAsync(request.Id, cancellationToken);
                if (Instractor == null)
                {
                    return new CommonResponse<GetInstructorByIdVm>
                    {
                        Success = false,
                        Message = "Instractor not found",
                        Data = null
                    };
                }

                var mappedStudent = _mapper.Map<GetInstructorByIdVm>(Instractor);

                return new CommonResponse<GetInstructorByIdVm>
                {
                    Success = true,
                    Message = "Instractor retrieved successfully",
                    Data = mappedStudent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Instractor");
                return new CommonResponse<GetInstructorByIdVm>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the Instractor",
                    Data = null
                };
            }
        }

    }
}
