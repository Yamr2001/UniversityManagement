using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.CreateInstructors
{
    public class CreateInstructorCommandHandler : ICommandHandler<CreateInstructorCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInstructorCommandHandler> _logger;

        public CreateInstructorCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateInstructorCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InstructorToCreate = _mapper.Map<Instructor>(request);
                await _uow.InstructorRepository.AddAsync(InstructorToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Instructor created successfully",
                    Data = InstructorToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating Instructor");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the Instructor",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
