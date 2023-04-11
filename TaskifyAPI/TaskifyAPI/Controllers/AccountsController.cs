using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Models.DTOs;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.UnitOfWorkService;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AccountUserDTO userdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                ApplicationUser user = new ApplicationUser(userdto);
                user.UserName = userdto.Email;
                var result = await _userManager.CreateAsync(user, userdto.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, "User");
                return Ok();
            }
            catch (Exception ex) {
                return Problem("Something went wrong", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO logindto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    logindto.Email, logindto.Password, false, false);

                if (!result.Succeeded || User.Identity.IsAuthenticated)
                {
                    return Unauthorized(logindto);
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _signInManager.SignOutAsync();
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }

    }
}


