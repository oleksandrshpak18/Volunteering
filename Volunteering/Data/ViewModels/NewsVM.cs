using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.ViewModels
{
    public class NewsVM
    {
        public Guid? NewsId { get; set; }
        public Guid? UserId { get; set; }
        [Required]
        public string NewsTitle { get; set; } = null!;
        [Required]
        public string NewsText { get; set; } = null!;
        
        [FromForm(Name = "NewsPhoto")] 
        public IFormFile? NewsPhoto { get; set; }  
        public string? NewsPhotoBase64 { get; set; }            
        public string? Author { get; set; }
        public string? CreateDate { get; set; }
    }
}
