using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;


namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext db;
        public UsersController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var users = db.Users.ToList();
            return Ok(users);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> AddUser(ApplicationUserDTO addUserRequest)
        {
            ApplicationUser au = new ApplicationUser(addUserRequest);
            await db.Users.AddAsync(au);
            await db.SaveChangesAsync();
            return Ok(addUserRequest);
        }
    }
}
