using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Models.DTOs
{
    public class ProjectDTO
    {

        [Required(ErrorMessage = "Please insert the project title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please insert the description of your project")]
        [StringLength(200, ErrorMessage = "The project description must have at most 200 characters")]
        public string Description { get; set; }

        public ProjectDTO(Project proj)
        {
            Title = proj.Title;
            Description = proj.Description;
        }
        public ProjectDTO() { }
    }
}
