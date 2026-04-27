using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Data
{
    public class SaflcDbContext : IdentityDbContext
    {
        public SaflcDbContext(DbContextOptions<SaflcDbContext> options) : base(options)
        {
        }
        public DbSet<Activities> tbl_Activities { get; set; }

        public DbSet<ActivityScore> tbl_ActivityScores { get; set; }

        public DbSet<Attendance> tbl_Attendances { get; set; }

        public DbSet<Billing> tbl_Billings { get; set; }

        public DbSet<Classes> tbl_Classes { get; set; }

        public DbSet<Enrollment> tbl_Enrollments { get; set; }

        public DbSet<Payment> tbl_Payments { get; set; }

        public DbSet<SchoolYear> tbl_SchoolYears { get; set; }

        public DbSet<Student> tbl_Students { get; set; }

        public DbSet<Subject> tbl_Subjects { get; set; }
        public DbSet<User> tbl_Users { get; set; }
    

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            // Get the current user name (You'll need to inject IHttpContextAccessor)
            //var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            var currentUser = "System  sample";

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.CreatedBy = currentUser;
                    // RowVersion is usually handled by the DB or set here
                    entity.RowVersion = Guid.NewGuid().ToByteArray();
                }

                entity.LastModifiedAt = DateTime.Now;
                entity.LastModifiedBy = currentUser;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
