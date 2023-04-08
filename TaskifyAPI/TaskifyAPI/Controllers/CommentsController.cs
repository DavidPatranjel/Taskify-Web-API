using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Models;

namespace TaskifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext db;
        public CommentsController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet(Name = "GetComments")]
        public async Task<IActionResult> GetComments()
        {
            var comms = db.Comments.ToList();
            return Ok(comms);
        }

        [HttpPost(Name = "AddComments")]
        public async Task<IActionResult> AddComments(Comment addCommentRequest)
        {
            await db.Comments.AddAsync(addCommentRequest);
            await db.SaveChangesAsync();
            return Ok(addCommentRequest);
        }
    }
}
