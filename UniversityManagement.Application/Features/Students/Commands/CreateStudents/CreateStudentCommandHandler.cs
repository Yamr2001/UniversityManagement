using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Commands.CreateStudents
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentsCommand, CommonResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public CreateStudentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateStudentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<int>> Handle(CreateStudentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentToCreate = _mapper.Map<Student>(request);
                await _uow.StudentRepository.AddAsync(StudentToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<int>
                {
                    Success = true,
                    Message = "Student created successfully",
                    Data = StudentToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating student");
                return new CommonResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the student",
                    Data = 0,
                };
                throw;
            }
        }
    }
}
