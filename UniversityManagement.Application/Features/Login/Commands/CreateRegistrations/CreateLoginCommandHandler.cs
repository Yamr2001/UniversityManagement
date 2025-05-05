using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Infrastructure.JWTGenrator;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Helpers;

namespace UniversityManagement.Application.Features.Login.Commands.CreateRegistrations
{
    public class CreateLoginCommandHandler : ICommandHandler<CreateLoginCommand, AuthResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly ILogger<CreateLoginCommandHandler> _logger;

        public CreateLoginCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateLoginCommandHandler> logger, IPasswordHasher passwordHasher, IJwtTokenGenerator tokenGenerator)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.GetByEmail(request.Email, cancellationToken);
                if (user == null || !_passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
                {
                    throw new UnauthorizedAccessException("Invalid credentials");
                }

                var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
                var token = _tokenGenerator.GenerateToken(user.Id, user.Email, roles);
                var refreshToken = _tokenGenerator.GenerateRefreshToken();

                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiryTime = refreshToken.Expires;

                await _uow.UserRepository.UpdateAsync(user, cancellationToken);
                await _uow.Complete();

                return new AuthResponse(token, refreshToken.Token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "Invalid credentials");
                throw new UnauthorizedAccessException("Invalid credentials");
                throw;
            }
        }
    }
}
