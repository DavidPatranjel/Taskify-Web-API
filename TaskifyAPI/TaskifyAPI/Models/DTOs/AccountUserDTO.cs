using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Please insert your Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class AccountUserDTO : LoginUserDTO
    {

        [Required(ErrorMessage = "Please insert your First Name"), MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please insert your Last Name"), MaxLength(100)]
        public string LastName { get; set; }
       
        public string PhoneNumber { get; set; }

    }
}
