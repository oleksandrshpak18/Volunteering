using Microsoft.AspNetCore.Mvc;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class ReportVM
    {
        public Guid ReportId { get; set; }
        public Guid CampaignId { get; set; }
        public string? ReportName { get; set; }
        public string? ReportDescription { get; set; }
        public DateTime CreateDate { get; set; }

        [FromForm(Name = "ReportPhotos")]
        public ICollection<IFormFile?> ReportPhotos { get; set; }
        public ICollection<string?> ?ReportPhotosBase64 { get; set; }
    }
}
