using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.ProjectsService
{
    public class ProjectsService : GenericService<Project>, IProjectsService
    {
        public ProjectsService(AppDbContext db) : base(db) { }
       
    }
}

