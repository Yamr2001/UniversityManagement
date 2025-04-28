using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Enrollments.Commands.CreateEnrollments
{
    public class CreateEnrollmentCommandHandler : ICommandHandler<CreateEnrollmentCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEnrollmentCommandHandler> _logger;

        public CreateEnrollmentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateEnrollmentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var EnrollmentToCreate = _mapper.Map<Enrollment>(request);
                await _uow.EnrollmentRepository.AddAsync(EnrollmentToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Enrollment created successfully",
                    Data = EnrollmentToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating Enrollment");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the Enrollment",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
