using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.ViewModels
{
    public class CampaignVM
    {
        public Guid CampaignId { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? CategoryId { get; set; }
        public string CampaignStatus { get; set; }
        public string CampaignPriority { get; set; }
        public string CampaignName { get; set; } = null!;
        public string CampaignDescription { get; set; } = null!;
        public string ApplianceDescription { get; set; } = null!;

        [FromForm(Name = "CampaignPhoto")]
        public IFormFile? CampaignPhoto { get; set; }
        public string? CampaignPhotoBase64 { get; set; }
        public decimal CampaignGoal { get; set; }
        public decimal? Accumulated { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime CreateDate { get; set; }
       
    }
}
