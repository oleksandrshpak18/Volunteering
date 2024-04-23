using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.ViewModels
{
    public class NewsVM
    {
        public int ?Id { get; set; }
        [Required]
        public string NewsTitle { get; set; } = null!;
        [Required]
        public string NewsText { get; set; } = null!;
        
        [FromForm(Name = "NewsPhoto")] 
        public IFormFile? NewsPhoto { get; set; }  
        public string? NewsPhotoBase64 { get; set; }
    }
}
