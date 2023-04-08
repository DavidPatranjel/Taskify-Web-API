using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models;
using Task = TaskifyAPI.Models.Task;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext db;
        public TasksController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet(Name = "GetTasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = db.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpPost(Name = "AddTask")]
        public async Task<IActionResult> AddTask(Task addTaskRequest)
        {
            await db.Tasks.AddAsync(addTaskRequest);
            await db.SaveChangesAsync();
            return Ok(addTaskRequest);
        }
    }
}
