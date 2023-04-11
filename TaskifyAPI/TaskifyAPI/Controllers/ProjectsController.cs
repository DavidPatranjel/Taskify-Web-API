using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.UnitOfWorkService;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private const string errorDbMessage = "DB Error: Cant find project with this id";

        public ProjectsController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = (await _unitOfWork.Projects.GetAll()).Select(a => new ProjectDTO(a)).ToList();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var proj = await _unitOfWork.Projects.GetById(id);

            if (proj == null)
            {
                return NotFound(errorDbMessage);
            }

            return new ProjectDTO(proj);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectDTO addProjectRequest)
        {
            Project p = new Project(addProjectRequest);
            var user_id = _unitOfWork.getUserManager().GetUserId(User);
            p.UserId = user_id;
            await _unitOfWork.Projects.Create(p);
            _unitOfWork.Save();
            return Ok(addProjectRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var proj = await _unitOfWork.Projects.GetById(id);

            if (proj == null)
            {
                return NotFound(errorDbMessage);
            }

            await _unitOfWork.Projects.Delete(proj);
            _unitOfWork.Save();
            return Ok(proj);
        }
    }
}
