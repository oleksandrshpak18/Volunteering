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
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
