
using TaskifyAPI.Services;

namespace TaskifyAPI.Services.UnitOfWorkService
{
    public interface IUnitOfWorkService:IDisposable
    {
        ProjectsService.IProjectsService Projects { get; }
        TasksService.ITasksService Tasks{ get; }
        CommentsService.ICommentsService Comments{ get; }
        UsersService.IUsersService Users{ get; }
        int Save();
    }
}
