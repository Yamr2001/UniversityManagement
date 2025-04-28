using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors
{
    public class UpdateInstructorCommandHandler : ICommandHandler<UpdateInstructorCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInstructorCommandHandler> _logger;

        public UpdateInstructorCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateInstructorCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Instractor = await _uow.InstructorRepository.GetByIdAsync(request.Id, cancellationToken);
                _mapper.Map(request, Instractor);
                await _uow.InstructorRepository.UpdateAsync(Instractor, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Student updated successfully",
                    Data = Instractor.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error updating Instractor");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the Instractor",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
