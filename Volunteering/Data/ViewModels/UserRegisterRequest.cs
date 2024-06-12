using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Volunteering.Data.ViewModels
{
    public class UserRegisterRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string UserSurname { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required]
        //[RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)", ErrorMessage = "Пароль повинен містити щонайменше 1 велику літеру, 1 малу літерута 1 цифру")]
        public string Password { get; set; } = null!;
    }
}
