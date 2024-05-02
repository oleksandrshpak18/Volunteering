using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Volunteering.Data.ViewModels
{
    public class UserDetailsVM
    {
        public Guid UserId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Speciality { get; set; }
        [Required]
        public string Organisation { get; set; }
        [Required]
        [FromForm(Name = "UserPhotoPassport")]
        public IFormFile UserPhotoPassport { get; set; }
        [Required]
        [FromForm(Name = "UserPhoto")]
        public IFormFile UserPhoto { get; set; }

        public string ?UserDescription {  get; set; }
    }
}
