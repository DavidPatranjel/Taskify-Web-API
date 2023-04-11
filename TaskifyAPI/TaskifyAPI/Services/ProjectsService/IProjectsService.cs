using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.ProjectsService
{
    public interface IProjectsService : IGenericService<Project>
    {
        Task<List<Models.Entities.Task>> GetTasksFromProject(int idproj);
        Task<List<Project>> GetProjectsFromUser(string userid);
    }
}
