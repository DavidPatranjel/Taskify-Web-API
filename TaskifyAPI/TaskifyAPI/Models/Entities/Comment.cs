using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.DTOs;

namespace TaskifyAPI.Models.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please insert your comment")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }
        public Comment(CommentDTO cdto)
        {
            Content = cdto.Content;
            Date = cdto.Date;
            TaskId = cdto.TaskId;
            UserId = cdto.UserId;
        }
        public Comment() { }

    }
}
