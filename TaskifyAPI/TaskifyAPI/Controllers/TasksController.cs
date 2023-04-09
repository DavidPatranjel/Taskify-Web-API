using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskDTO = TaskifyAPI.Models.DTOs.TaskDTO;
using Task = TaskifyAPI.Models.Entities.Task;

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
        public async Task<IActionResult> AddTask(TaskDTO addTaskRequest)
        {
            Task t = new Task(addTaskRequest);
            await db.Tasks.AddAsync(t);
            await db.SaveChangesAsync();
            return Ok(addTaskRequest);
        }
    }
}
