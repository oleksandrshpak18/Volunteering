using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int? UserRoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public byte[]? UserPhoto { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CardNumber { get; set; }
        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? City { get; set; }
        public float? Rating { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual UserRole? UserRole { get; set; }
    }
}
