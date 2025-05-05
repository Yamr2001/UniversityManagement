using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Domain.Entities.Roles;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Domain.Entities.UserRoles;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Shared.Comman;

namespace UniversityManagement.Infrastructure.Presistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Address);

            modelBuilder.Entity<Instructor>()
                .OwnsOne(i => i.OfficeAssignment);

            // Apply global query filter for soft delete
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Enrollment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Instructor>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => !ur.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditEntity<int> intEntity)
                {
                    ProcessAuditEntity(intEntity, entry, now);
                }
                else if (entry.Entity is AuditEntity<Guid> guidEntity)
                {
                    ProcessAuditEntity(guidEntity, entry, now);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessAuditEntity<T>(AuditEntity<T> entity, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, DateTime now)
        {
            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = now;
                entity.IsDeleted = false;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedDate = now;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entity.IsDeleted = true;
                entity.UpdatedDate = now;
            }
        }
    }
}