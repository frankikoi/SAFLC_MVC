using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAFLC_MVC.Application.Model;

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
    }
}
