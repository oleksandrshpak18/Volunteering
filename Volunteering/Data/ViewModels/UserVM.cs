﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class UserVM
    {
        public Guid? UserId { get; set; }
        
        public string UserName { get; set; } = null!;
        
        public string UserSurname { get; set; } = null!;

        [FromForm(Name ="UserPhoto")]
        public IFormFile? UserPhoto { get; set; }
        public string? UserPhotoBase64 { get; set; }
        
        [FromForm(Name = "UserPhotoPassport")]
        public IFormFile? UserPhotoPassport { get; set; }
        public string? UserPhotoPassportBase64 { get; set; }
        
        public string Email { get; set; } = null!;
        
        public string ?Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Organisation { get; set; }
        public string? Speciality { get; set; }
        public string? CardNumber { get; set; }
        public string? City { get; set; }
        public string? DateJoined { get; set; }
        public float? Rating { get; set; }
        public string? UserDescriptioin { get; set; }
        
    }
}
