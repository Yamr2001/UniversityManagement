using UniversityManagement.Domain.Entities.Roles;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Domain.Entities.UserRoles
{
    public class UserRole : AuditEntity<int>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
