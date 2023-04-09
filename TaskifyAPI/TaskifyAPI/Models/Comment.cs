using System.ComponentModel.DataAnnotations;

namespace TaskifyAPI.Models
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please insert your comment")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int? TaskId { get; set; }

        /// <summary>
        /// public virtual Task? Task { get; set; }
        /// </summary>

        /*Persoana care comenteaza*/
        public int? UserId { get; set; } 

        ///public virtual User? User { get; set; }
    }
}
