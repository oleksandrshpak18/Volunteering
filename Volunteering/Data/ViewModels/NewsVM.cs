using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.ViewModels
{
    public class NewsVM
    {
        public string NewsTitle { get; set; } = null!;
        public string NewsText { get; set; } = null!;

        
        [FromForm(Name = "NewsPhoto")] 
        public IFormFile? NewsPhoto { get; set; }  
        public string? NewsPhotoBase64 { get; set; }
    }
}
