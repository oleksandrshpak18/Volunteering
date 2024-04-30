using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class User
    {
        public User()
        {
            Donations = new HashSet<Donation>();
            News = new HashSet<News>();
            UserCampaigns = new HashSet<UserCampaign>();
        }

        public Guid UserId { get; set; }
        public Guid? UserRoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public byte[]? UserPhoto { get; set; }
        public byte[]? UserPhotoPassport { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CardNumber { get; set; }
        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? City { get; set; }
        public string? UserDescription { get; set; }
        public float? Rating { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual UserRole? UserRole { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<UserCampaign> UserCampaigns { get; set; }
    }
}
