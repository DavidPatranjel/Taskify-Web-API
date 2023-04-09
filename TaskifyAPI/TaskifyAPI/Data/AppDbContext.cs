using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Models.Entities.Task> Tasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // definire primary key compus
            modelBuilder.Entity<UserProject>()
            .HasKey(ab => new {
                ab.UserId,
                ab.ProjectId
            });
        }      

    }
}
