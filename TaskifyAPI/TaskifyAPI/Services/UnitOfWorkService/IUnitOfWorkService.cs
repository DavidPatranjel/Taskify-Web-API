
using Microsoft.AspNetCore.Identity;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services;

namespace TaskifyAPI.Services.UnitOfWorkService
{
    public interface IUnitOfWorkService:IDisposable
    {
        ProjectsService.IProjectsService Projects { get; }
        TasksService.ITasksService Tasks{ get; }
        CommentsService.ICommentsService Comments{ get; }
        UsersService.IUsersService Users{ get; }
        UserManager<ApplicationUser> getUserManager();
        int Save();
    }
}
