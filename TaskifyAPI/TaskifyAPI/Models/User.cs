using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskifyAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserProject>? UserProjects { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? AllRoles { get; set; }

        public virtual ICollection<Project>? Projects { get; set; }

        public virtual ICollection<Task>? Tasks { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
