using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.DTOs;

namespace TaskifyAPI.Models.Entities
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

        public string UserId { get; set; }
        public Project(ProjectDTO pdto)
        {
            Title = pdto.Title;
            Description = pdto.Description;
        }
        public Project() { }

    }
}
