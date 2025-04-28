using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments
{
    public class UpdateEnrollmentCommandHandler : ICommandHandler<UpdateEnrollmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEnrollmentCommandHandler> _logger;

        public UpdateEnrollmentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateEnrollmentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Enrollment = await _uow.EnrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
                _mapper.Map(request, Enrollment);
                await _uow.EnrollmentRepository.UpdateAsync(Enrollment, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Enrollment updated successfully",
                    Data = Enrollment.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error updating Enrollment");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the Enrollment",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
