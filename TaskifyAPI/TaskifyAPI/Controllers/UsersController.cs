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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private const string errorDbMessage = "DB Error: Cant find user with this id";

        public UsersController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetUsers()
        {
            var users = (await _unitOfWork.Users.GetAll()).Select(a => new ApplicationUserDTO(a)).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApplicationUserDTO>> GetUser(string id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound(errorDbMessage);
            }

            return new ApplicationUserDTO(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound(errorDbMessage);
            }

            if (user.Id == _unitOfWork.getUserManager().GetUserId(User))
            {
                return Unauthorized();
            }

            var projects = await _unitOfWork.Projects.GetProjectsFromUser(id);

            foreach(var proj in projects){
                var tasks = await _unitOfWork.Projects.GetTasksFromProject(proj.Id);
                foreach (var task in tasks)
                {
                    var comms = await _unitOfWork.Tasks.GetCommentsFromTask(task.Id);
                    foreach(var comm in comms){
                        await _unitOfWork.Comments.Delete(comm);
                    }
                    await _unitOfWork.Tasks.Delete(task);
                }

                var team = await _unitOfWork.UserProjects.GetTeamFromProject(proj.Id);
                foreach(var elem in team)
                {
                    await _unitOfWork.UserProjects.Delete(elem);
                }

                await _unitOfWork.Projects.Delete(proj);
            }

            var team2 = await _unitOfWork.UserProjects.GetTeamFromUser(user.Id);
            foreach (var elem in team2)
            {
                await _unitOfWork.UserProjects.Delete(elem);
            }

            var commsuser = await _unitOfWork.Comments.GetCommentsOfUser(user.Id);
            foreach( var elem in commsuser)
            {
                await _unitOfWork.Comments.Delete(elem);
            }



            await _unitOfWork.Users.Delete(user);
            _unitOfWork.Save();
            return Ok(user);
        }

    }
}
