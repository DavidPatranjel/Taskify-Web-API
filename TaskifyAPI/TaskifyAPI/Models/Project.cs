using System.ComponentModel.DataAnnotations;

namespace TaskifyAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please insert the project title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please insert the description of your project")]
        [StringLength(200, ErrorMessage = "The project description must have at most 200 characters")]
        public string Description { get; set; }



        /*Persoana care creaza proiectul*/
        public int? UserId { get; set; } 

        public virtual User? User { get; set; }


        public virtual ICollection<Task>? Tasks { get; set; }
        public virtual ICollection<UserProject>? UserProjects { get; set; }
    }
}
