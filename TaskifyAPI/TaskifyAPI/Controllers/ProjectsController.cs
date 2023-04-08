using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models;

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
        public async Task<IActionResult> AddProject(Project addProjectRequest)
        {
            await db.Projects.AddAsync(addProjectRequest);
            await db.SaveChangesAsync();
            return Ok(addProjectRequest);
        }
    }
}
