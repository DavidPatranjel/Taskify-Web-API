using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Models.DTOs
{
    public class CommentDTO
    {

        [Required(ErrorMessage = "Please insert your comment")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public CommentDTO(Comment comm)
        {
            Content = comm.Content;
            Date = comm.Date;
            TaskId = comm.TaskId;
            UserId = comm.UserId;
        }
        public CommentDTO() { }

    }
}
