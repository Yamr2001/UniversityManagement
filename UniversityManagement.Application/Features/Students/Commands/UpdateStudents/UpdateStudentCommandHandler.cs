using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Commands.UpdateStudents
{
    public class UpdateStudentCommandHandler : ICommandHandler<UpdateStudentsCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStudentCommandHandler> _logger;

        public UpdateStudentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateStudentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(UpdateStudentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Student = await _uow.StudentRepository.GetByIdAsync(request.Id, cancellationToken);
                _mapper.Map(request, Student);
                await _uow.StudentRepository.UpdateAsync(Student, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Student updated successfully",
                    Data = Student.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error updating student");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the student",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
