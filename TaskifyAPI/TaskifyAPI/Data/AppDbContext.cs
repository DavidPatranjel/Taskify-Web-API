using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace TaskifyAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // definire primary key compus
            builder.Entity<UserProject>()
            .HasKey(ab => new {
                ab.UserId,
                ab.ProjectId
            });
        }      

    }
}
