using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Students.Quries.GetStudentById
{
    public class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery, CommonResponse<GetStudentsByIdVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStudentByIdQueryHandler> _logger;

        public GetStudentByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetStudentByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommonResponse<GetStudentsByIdVm>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _uow.StudentRepository.GetByIdAsync(request.Id, cancellationToken);
                if (student == null)
                {
                    return new CommonResponse<GetStudentsByIdVm>
                    {
                        Success = false,
                        Message = "Student not found",
                        Data = null
                    };
                }

                var mappedStudent = _mapper.Map<GetStudentsByIdVm>(student);

                return new CommonResponse<GetStudentsByIdVm>
                {
                    Success = true,
                    Message = "Student retrieved successfully",
                    Data = mappedStudent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student");
                return new CommonResponse<GetStudentsByIdVm>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the student",
                    Data = null
                };
            }
        }

    }
}
