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
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private const string errorDbMessage = "DB Error: Cant find comment with this id";

        public CommentsController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comms = (await _unitOfWork.Comments.GetAll()).Select(a => new CommentDTO(a)).ToList();
            return Ok(comms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            var comm = await _unitOfWork.Comments.GetById(id);

            if (comm == null)
            {
                return NotFound(errorDbMessage);
            }

            return new CommentDTO(comm);
        }

        [HttpPost(Name = "AddComment")]
        public async Task<IActionResult> AddComments(CommentDTO addCommentRequest)
        {
            Comment c = new Comment(addCommentRequest);
            await _unitOfWork.Comments.Create(c);
            _unitOfWork.Save();
            return Ok(addCommentRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comm = await _unitOfWork.Comments.GetById(id);

            if (comm == null)
            {
                return NotFound(errorDbMessage);
            }

            await _unitOfWork.Comments.Delete(comm);
            _unitOfWork.Save();
            return Ok(comm);
        }
    }
}
