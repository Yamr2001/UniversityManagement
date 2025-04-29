using Microsoft.EntityFrameworkCore;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Domain.Entities.Students;
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

            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Enrollment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Instructor>().HasQueryFilter(i => !i.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is AuditEntity<int> &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (AuditEntity<int>)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedDate = now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
