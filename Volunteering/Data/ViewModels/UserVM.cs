using Microsoft.AspNetCore.Mvc;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class UserVM
    {
        public int? UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;

        [FromForm(Name ="UserPhoto")]
        public IFormFile? UserPhoto { get; set; }
        public string? UserPhotoBase64 { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? City { get; set; }
        public string? DateJoined { get; set; }
        public float? Rating { get; set; }
        
    }
}
