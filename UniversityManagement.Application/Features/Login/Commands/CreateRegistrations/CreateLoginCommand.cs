using UniversityManagement.Shared.Application.Abstractions.Messeging;

namespace UniversityManagement.Application.Features.Login.Commands.CreateRegistrations
{
    public class CreateLoginCommand : ICommand<AuthResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
