using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TaskifyAPI.Models.DTOs;

namespace TaskifyAPI.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please insert your First Name"), MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please insert your Last Name"), MaxLength(100)]
        public string LastName { get; set; }
        public ApplicationUser(ApplicationUserDTO audto)
        {
            FirstName = audto.FirstName;
            LastName = audto.LastName;
            Email = audto.Email;
        }
        public ApplicationUser() { }
    }
}
