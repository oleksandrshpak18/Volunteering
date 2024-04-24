using System.ComponentModel.DataAnnotations;

namespace Volunteering.Data.ViewModels
{
    public class UserLoginRequestVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
