using System.Security.Claims;
using UniversityManagement.Domain;

namespace UniversityManagement.Infrastructure.JWTGenrator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, ICollection<string> roles);
        RefreshToken GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
