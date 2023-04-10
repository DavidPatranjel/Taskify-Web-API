using TaskifyAPI.Data;
using TaskifyAPI.Services;

namespace TaskifyAPI.Services.UnitOfWorkService
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext db;
        public UnitOfWorkService(AppDbContext db)
        {
            this.db = db;
            Projects = new ProjectsService.ProjectsService(this.db);
            Tasks = new TasksService.TasksService(this.db);
            Comments = new CommentsService.CommentsService(this.db);
            Users = new UsersService.UsersService(this.db);
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
