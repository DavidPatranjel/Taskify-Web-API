using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.UnitOfWorkService;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<CommentsController> _logger;
        private const string errorDbMessage = "DB Error: Cant find comment with this id";
        private const string errorDbMessage2 = "DB Error: Cant find task with this id";

        public CommentsController(IUnitOfWorkService unitOfWork,
            ILogger<CommentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        [HttpGet("/comments/{task_id}")]
        public async Task<IActionResult> GetComments(int task_id)
        {
            _logger.LogDebug("Running getting comments of a task...");
            var task = await _unitOfWork.Tasks.GetById(task_id);

            if(task == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage2);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin")) { 
                var comms = (await _unitOfWork.Comments.GetAll())
                        .Where(a => a.TaskId == task_id).Select(a => new CommentDTO(a)).ToList();
                return Ok(comms);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }

        [HttpGet("/comm/{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            _logger.LogDebug("Running getting a comment...");
            var comm = await _unitOfWork.Comments.GetById(id);
            

            if (comm == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var task = await _unitOfWork.Tasks.GetById(comm.TaskId);
            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin"))
            {
                return new CommentDTO(comm);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }

           
        }

        [HttpPost("{taskid}")]
        public async Task<IActionResult> AddComments(int taskid, [FromBody] CommentDTO addCommentRequest)
        {
            _logger.LogDebug("Running adding a comment...");
            var task = await _unitOfWork.Tasks.GetById(taskid);
            if(task == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage2);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var usersinproj = await _unitOfWork.UserProjects.GetUsersInProject(task.ProjectId);

            if (usersinproj.Contains(user_id) || User.IsInRole("Admin"))
            {
                Comment c = new Comment(addCommentRequest);
                c.UserId = user_id;
                c.TaskId = taskid;
                await _unitOfWork.Comments.Create(c);
                _unitOfWork.Save();
                return Ok(addCommentRequest);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] CommentDTO newcomm)
        {
            _logger.LogDebug("Running updating a comment...");
            var comm = await _unitOfWork.Comments.GetById(id);

            if (comm == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var task = await _unitOfWork.Tasks.GetById(comm.TaskId);
            var project = await _unitOfWork.Projects.GetById(task.ProjectId);

            if (comm.UserId == user_id || project.UserId == user_id || User.IsInRole("Admin"))
            {
                comm.Content = newcomm.Content;
                comm.Date = newcomm.Date;

                await _unitOfWork.Comments.Update(comm);
                _unitOfWork.Save();

                return Ok(comm);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            _logger.LogDebug("Running deleting a comment...");
            var comm = await _unitOfWork.Comments.GetById(id);

            if (comm == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            var task = await _unitOfWork.Tasks.GetById(comm.TaskId);
            var project = await _unitOfWork.Projects.GetById(task.ProjectId);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);

            if (comm.UserId == user_id || project.UserId == user_id || User.IsInRole("Admin"))
            {

                await _unitOfWork.Comments.Delete(comm);
                _unitOfWork.Save();
                return Ok(comm);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }
    }
}
