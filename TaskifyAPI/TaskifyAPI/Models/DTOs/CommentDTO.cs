using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Models.DTOs
{
    public class CommentDTO
    {

        [Required(ErrorMessage = "Please insert your comment")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public CommentDTO(Comment comm)
        {
            Content = comm.Content;
            Date = comm.Date;
        }
        public CommentDTO() { }

    }
}
