using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class UserVM
    {
        public int? UserId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string UserSurname { get; set; } = null!;

        [FromForm(Name ="UserPhoto")]
        public IFormFile? UserPhoto { get; set; }
        public string? UserPhotoBase64 { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string ?Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? City { get; set; }
        public string? DateJoined { get; set; }
        public float? Rating { get; set; }
        
    }
}
