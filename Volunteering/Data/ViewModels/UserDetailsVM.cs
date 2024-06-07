using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Volunteering.Data.ViewModels
{
    public class UserDetailsVM
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Номер телефону є обов'язковим")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Номер картки є обов'язковим")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Місто є обов'язковим")]
        public string City { get; set; }
        [Required(ErrorMessage = "Посада є обов'язковою")]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "Організація є обов'язковою")]
        public string Organisation { get; set; }
        [Required(ErrorMessage = "Фото паспорта є обов'язковим")]
        [FromForm(Name = "UserPhotoPassport")]
        public IFormFile UserPhotoPassport { get; set; }
        [Required(ErrorMessage = "Фото профіля є обов'язковим")]
        [FromForm(Name = "UserPhoto")]
        public IFormFile UserPhoto { get; set; }

        public string ?UserDescription {  get; set; }
    }
}
