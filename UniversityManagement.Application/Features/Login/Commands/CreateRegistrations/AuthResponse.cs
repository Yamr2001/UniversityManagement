namespace UniversityManagement.Application.Features.Login.Commands.CreateRegistrations
{
    public class AuthResponse(string token, string refreshtoken)
    {
        public string Token { get; set; } = token;
        public string RefreshToken { get; set; } = refreshtoken;
    }
}
