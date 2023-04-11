using Microsoft.AspNetCore.Identity;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services;

namespace TaskifyAPI.Services.UnitOfWorkService
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UnitOfWorkService(
            AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            Projects = new ProjectsService.ProjectsService(this.db);
            Tasks = new TasksService.TasksService(this.db);
            Comments = new CommentsService.CommentsService(this.db);
            Users = new UsersService.UsersService(this.db);
            _roleManager = roleManager;
        }
        public ProjectsService.IProjectsService Projects { 
            get;
            private set;
        }
        public TasksService.ITasksService Tasks
        {
            get;
            private set;
        }
        public CommentsService.ICommentsService Comments
        {
            get;
            private set;
        }
        public UsersService.IUsersService Users
        {
            get;
            private set;
        }
        public UserManager<ApplicationUser> getUserManager()
        {
            return _userManager;
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
