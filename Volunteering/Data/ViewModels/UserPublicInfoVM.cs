using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.ViewModels
{
    public class UserPublicInfoVM
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserSurname { get; set; } = null!;

        public string? UserPhotoBase64 { get; set; }

        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? City { get; set; }
        public string? DateJoined { get; set; }
        public float? Rating { get; set; }
        public string? UserDescriptioin { get; set; }
    }
}
