using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.UserProjectService
{
    public interface IUserProjectService: IGenericService<UserProject>
    {
        Task<List<string>> GetUsersInProject(int projid);
        Task<List<int>> GetProjectsOfUser(string userid);
        Task<List<UserProject>> GetTeamFromProject(int projid);
        Task<List<UserProject>> GetTeamFromUser(string userid);

    }
}
