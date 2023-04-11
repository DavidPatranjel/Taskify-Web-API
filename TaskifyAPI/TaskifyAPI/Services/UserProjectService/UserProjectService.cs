using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;
using TaskifyAPI.Services.ProjectsService;

namespace TaskifyAPI.Services.UserProjectService
{
    public class UserProjectService : GenericService<UserProject>, IUserProjectService
    {
        public UserProjectService(AppDbContext db) : base(db) { }
        public async Task<List<string>> GetUsersInProject(int projid)
        {
            return await _db.UserProjects.Where(userpr => userpr.ProjectId == projid)
                .Select(user => user.UserId)
                .ToListAsync();
        }
        public async Task<List<int>> GetProjectsOfUser(string userid)
        {
            return await _db.UserProjects.Where(userpr => userpr.UserId == userid)
                .Select(user => user.ProjectId)
                .ToListAsync();
        }

        public async Task<List<UserProject>> GetTeamFromProject(int projid)
        {
            return await _db.UserProjects.Where(userpr => userpr.ProjectId == projid)
                .ToListAsync();
        }
        public async Task<List<UserProject>> GetTeamFromUser(string userid)
        {
            return await _db.UserProjects.Where(userpr => userpr.UserId == userid)
                .ToListAsync();
        }
    }
}
