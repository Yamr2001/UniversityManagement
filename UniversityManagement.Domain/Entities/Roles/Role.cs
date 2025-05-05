using UniversityManagement.Domain.Entities.UserRoles;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Domain.Entities.Roles
{
    public class Role : AuditEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
