using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskifyAPI.Data;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.UnitOfWorkService;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<ProjectsController> _logger;
        private const string errorDbMessage = "DB Error: Cant find project with this id";
        private const string errorDbMessage2 = "DB Error: Cant find user with this id";

        public ProjectsController(IUnitOfWorkService unitOfWork,
                                    ILogger<ProjectsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            _logger.LogDebug("Running getting all projects of a user...");
            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var listprojs = (await _unitOfWork.UserProjects.GetProjectsOfUser(user_id));
            var projects = (await _unitOfWork.Projects.GetAll()).Where(proj => listprojs.Contains(proj.Id)).Select(a => new ProjectDTO(a)).ToList();
            return Ok(projects);
        }

        [HttpGet]
        [Route("adminprojects")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProjectsAdmin()
        {
            _logger.LogDebug("Running getting all projects (ADMIN ONLY)...");
            var projects = (await _unitOfWork.Projects.GetAll()).Select(a => new ProjectDTO(a)).ToList();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            _logger.LogDebug("Running getting a project...");
            var proj = await _unitOfWork.Projects.GetById(id);

            if (proj == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var users = await _unitOfWork.UserProjects.GetUsersInProject(id);
            if (users.Contains(user_id) || User.IsInRole("Admin")) {
                return new ProjectDTO(proj);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectDTO addProjectRequest)
        {
            _logger.LogDebug("Running adding a project...");
            Project p = new Project(addProjectRequest);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            p.UserId = user_id;
            await _unitOfWork.Projects.Create(p);
            _unitOfWork.Save();
            _logger.LogInformation("Adding project to DB...");


            UserProject userProject = new UserProject();
            userProject.UserId = p.UserId;
            userProject.ProjectId = p.Id;

            await _unitOfWork.UserProjects.Create(userProject);
            _unitOfWork.Save();
            _logger.LogInformation("Adding team to DB...");

            return Ok(addProjectRequest);
        }

        [HttpPost]
        [Route("adduserinproject")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<UserProject>> AddUsersInProject([FromBody] UserProject uspr)
        {
            _logger.LogDebug("Running adding a user in the project team...");
            var proj = await _unitOfWork.Projects.GetById(uspr.ProjectId);
            var usr = await _unitOfWork.Users.GetById(uspr.UserId);

            if (proj == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }
            if (usr == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage2);
            }

            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var users = await _unitOfWork.UserProjects.GetUsersInProject(proj.Id);

            if (proj.UserId == user_id || User.IsInRole("Admin"))
            {
                if (users.Contains(uspr.UserId))
                {
                    return Unauthorized("This user is already in this team");
                }

                await _unitOfWork.UserProjects.Create(uspr);
                _unitOfWork.Save();
                return Ok(uspr);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> PutProject(int id, [FromBody] ProjectDTO newproj)
        {
            _logger.LogDebug("Running updating a project...");
            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            var project = await _unitOfWork.Projects.GetById(id);

            if (project == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            if (project.UserId == user_id || User.IsInRole("Admin"))
            {
                project.Title = newproj.Title;
                project.Description = newproj.Description;

                await _unitOfWork.Projects.Update(project);
                _unitOfWork.Save();

                return Ok(project);
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            _logger.LogDebug("Running deleting a project...");
            var proj = await _unitOfWork.Projects.GetById(id);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);


            if (proj == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            if (proj.UserId == user_id || User.IsInRole("Admin"))
            {
                var listatasks = await _unitOfWork.Projects.GetTasksFromProject(id);

                foreach (var task in listatasks)
                {
                    var listacomms = await _unitOfWork.Tasks.GetCommentsFromTask(task.Id);

                    foreach (var comm in listacomms)
                    {
                        await _unitOfWork.Comments.Delete(comm);
                    }

                    await _unitOfWork.Tasks.Delete(task);
                }

                var team = await _unitOfWork.UserProjects.GetTeamFromProject(proj.Id);
                foreach (var elem in team)
                {
                    await _unitOfWork.UserProjects.Delete(elem);
                }

                await _unitOfWork.Projects.Delete(proj);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }
        }
    }
}
