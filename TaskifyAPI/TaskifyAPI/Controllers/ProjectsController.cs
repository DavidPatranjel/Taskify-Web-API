using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext db;
        public ProjectsController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet(Name = "GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = db.Projects.ToList();
            return Ok(projects);
        }

        [HttpPost(Name = "AddProject")]
        public async Task<IActionResult> AddProject(ProjectDTO addProjectRequest)
        {
            Project p = new Project(addProjectRequest);
            await db.Projects.AddAsync(p);
            await db.SaveChangesAsync();
            return Ok(addProjectRequest);
        }
    }
}
