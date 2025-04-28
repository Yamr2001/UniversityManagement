using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById
{
    public class GetEnrollmentByIdQueryHandler : IQueryHandler<GetEnrollmentByIdQuery, CommonResponse<GetEnrollmentByIdVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEnrollmentByIdQueryHandler> _logger;

        public GetEnrollmentByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetEnrollmentByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<GetEnrollmentByIdVm>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Enrollment = await _uow.EnrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
                if (Enrollment == null)
                {
                    return new CommonResponse<GetEnrollmentByIdVm>
                    {
                        Success = false,
                        Message = "Enrollment not found",
                        Data = null
                    };
                }

                var mappedStudent = _mapper.Map<GetEnrollmentByIdVm>(Enrollment);

                return new CommonResponse<GetEnrollmentByIdVm>
                {
                    Success = true,
                    Message = "Enrollment retrieved successfully",
                    Data = mappedStudent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Enrollment");
                return new CommonResponse<GetEnrollmentByIdVm>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the Enrollment",
                    Data = null
                };
            }
        }

    }
}
