using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.ProjectsService
{
    public class ProjectsService : GenericService<Project>, IProjectsService
    {
        public ProjectsService(AppDbContext db) : base(db) { }
        public async Task<List<Models.Entities.Task>> GetTasksFromProject(int idproj)
        {
            return await _db.Tasks.Where(a => a.ProjectId== idproj).ToListAsync();
        }

        public async Task<List<Project>> GetProjectsFromUser(string userid)
        {
            return await _db.Projects.Where(a => a.UserId == userid).ToListAsync();
        }

    }
}

