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
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly string errorDbMessage = "DB Error: Cant find task with this id";
        private readonly string errorDbMessageproj = "DB Error: Cant find project with this id";
        public TasksController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/tasks/{projid}")]
        public async Task<IActionResult> GetTasks(int projid)
        {
            var proj = await _unitOfWork.Projects.GetById(projid);

            if (proj == null)
            {
                return NotFound(errorDbMessageproj);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(projid);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin"))
            {
                var tasks = (await _unitOfWork.Tasks.GetAll())
                            .Where(a => a.ProjectId == projid).Select(a => new TaskDTO(a)).ToList();
                return Ok(tasks);
            }
            else
            {
                return Unauthorized();
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin"))
            {

                return new TaskDTO(task);
            }
            else
            {
                return Unauthorized();
            }
        }
       

        [HttpPost("{projid}")]
        public async Task<IActionResult> AddTaskToProject(int projid, [FromBody] TaskDTO addTaskRequest)
        {
            var proj = await _unitOfWork.Projects.GetById(projid);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);

            if (proj == null)
            {
                return NotFound(errorDbMessageproj);
            }


            if (proj.UserId == user_id || User.IsInRole("Admin"))
            {
                Task t = new Task(addTaskRequest);
                t.ProjectId = projid;
                t.UserId = user_id;

                await _unitOfWork.Tasks.Create(t);
                _unitOfWork.Save();
                return Ok(addTaskRequest);
            }
            else
            {
                return Unauthorized();
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, [FromBody] TaskDTO newtask)
        {
            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }
            var project = await _unitOfWork.Projects.GetById(task.ProjectId);

            if (project.UserId == user_id || User.IsInRole("Admin"))
            {
                task.Title = newtask.Title;
                task.Description = newtask.Description;
                task.Status = (Task.TaskStatus)newtask.Status;
                task.StartDate = newtask.StartDate;
                task.EndDate = newtask.EndDate;

                await _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();

                return Ok(task);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("/statustask/{taskid}")]
        public async Task<IActionResult> PutStatusTask(int taskid, [FromBody] TaskStatusDTO newtask)
        {
            var task = await _unitOfWork.Tasks.GetById(taskid);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin"))
            {
                task.Status = (Task.TaskStatus)newtask.Status;
                await _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();

                return Ok(task);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("/userintask/{taskid}/{userid}")]
        public async Task<IActionResult> PutUserTask(int taskid, string userid)
        {

            var task = await _unitOfWork.Tasks.GetById(taskid);
            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var project = await _unitOfWork.Projects.GetById(task.ProjectId);
            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);

            if (!usersinproj.Contains(userid))
            {
                return Unauthorized();
            }

            if (project.UserId == user_id || User.IsInRole("Admin"))
            {
                task.UserId = userid;
                await _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();

                return Ok(task);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                return NotFound(errorDbMessage);
            }

            var project = await _unitOfWork.Projects.GetById(task.ProjectId);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);

            if (project.UserId == user_id || User.IsInRole("Admin"))
            {

                var listacomms = await _unitOfWork.Tasks.GetCommentsFromTask(id);

                foreach (var comm in listacomms)
                {
                    await _unitOfWork.Comments.Delete(comm);
                }

                await _unitOfWork.Tasks.Delete(task);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
