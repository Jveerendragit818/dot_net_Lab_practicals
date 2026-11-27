using Microsoft.EntityFrameworkCore;
using SmartCampusServicePortal.Models;

namespace SmartCampusServicePortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<CampusEvent> CampusEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequests");

            modelBuilder.Entity<Notice>().ToTable("Notices");

            modelBuilder.Entity<CampusEvent>().ToTable("CampusEvents");

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}