using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Helpers;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Application.Features.Registration.Commands.CreateRegistrations
{
    public class CreateRegistrationCommandHandler : ICommandHandler<CreateRegistrationCommand, CommonResponse<Guid>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<CreateRegistrationCommandHandler> _logger;

        public CreateRegistrationCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateRegistrationCommandHandler> logger, IPasswordHasher passwordHasher)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<CommonResponse<Guid>> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserToCreate = _mapper.Map<User>(request);
                UserToCreate.PasswordHash = _passwordHasher.HashPassword(request.Password);
                await _uow.UserRepository.AddAsync(UserToCreate, cancellationToken);
                await _uow.Complete();

                return new CommonResponse<Guid>
                {
                    Success = true,
                    Message = "User created successfully",
                    Data = UserToCreate.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Error creating User");
                return new CommonResponse<Guid>
                {
                    Success = false,
                    Message = "An error occurred while creating the User",
                    Data = Guid.Empty,
                };
                throw;
            }
        }
    }
}
