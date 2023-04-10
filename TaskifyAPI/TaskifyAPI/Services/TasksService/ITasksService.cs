using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.TasksService
{
    public interface ITasksService : IGenericService<Models.Entities.Task>
    {
        Task<List<Models.Entities.Task>> GetTaskOfProject(int projid);
    }
}
