using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskDTO = TaskifyAPI.Models.DTOs.TaskDTO;
using Task = TaskifyAPI.Models.Entities.Task;
using TaskifyAPI.Services.UnitOfWorkService;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Threading.Tasks;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly string errorDbMessage = "DB Error: Cant find task with this id";
        private readonly string errorDbMessageproj = "DB Error: Cant find project with this id";
        public TasksController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = (await _unitOfWork.Tasks.GetAll()).Select(a => new TaskDTO(a)).ToList();
            return Ok(tasks);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            return new TaskDTO(task);
        }

        [HttpGet("gettasksprojects/{projid}")]
        public async Task<IActionResult> GetTasksOfProject(int projid)
        {
            var tasks = (await _unitOfWork.Tasks.GetTaskOfProject(projid)).Select(a => new TaskDTO(a)).ToList();
            if (tasks == null)
            {
                return NotFound(errorDbMessage);
            }
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskDTO addTaskRequest)
        {
            Task t = new Task(addTaskRequest);
            await _unitOfWork.Tasks.Create(t);
            _unitOfWork.Save();
            return Ok(addTaskRequest);
        }

        [HttpPost("addtasksprojects/{projid}")]
        public async Task<IActionResult> AddTaskToProject(int projid, TaskDTO addTaskRequest)
        {
            var proj = await _unitOfWork.Projects.GetById(projid);

            if (proj == null)
            {
                return NotFound(errorDbMessageproj);
            }

            Task t = new Task(addTaskRequest);
            t.ProjectId = projid;

            await _unitOfWork.Tasks.Create(t);
            _unitOfWork.Save();
            return Ok(addTaskRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            await _unitOfWork.Tasks.Delete(task);
            _unitOfWork.Save();
            return Ok(task);
        }
    }
}
