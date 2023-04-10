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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private const string errorDbMessage = "DB Error: Cant find user with this id";

        public UsersController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = (await _unitOfWork.Users.GetAll()).Select(a => new ApplicationUserDTO(a)).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO>> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound(errorDbMessage);
            }

            return new ApplicationUserDTO(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(ApplicationUserDTO addUserRequest)
        {
            ApplicationUser au = new ApplicationUser(addUserRequest);
            await _unitOfWork.Users.Create(au);
            _unitOfWork.Save();
            return Ok(addUserRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound(errorDbMessage);
            }

            await _unitOfWork.Users.Delete(user);
            _unitOfWork.Save();
            return Ok(user);
        }

    }
}
