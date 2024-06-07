using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Volunteering.Data.ViewModels
{
    public class CampaignVM
    {
        public Guid CampaignId { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? UserId { get; set; }
        public ReportVM ?Report { get; set; }
        public string? Author {  get; set; }
        
        public string? Category { get; set; }
        [Required(ErrorMessage = "Підкатегорія обов'язкова")]
        public string? Subcategory { get; set; }
        public string? CampaignStatus { get; set; }
        [Required(ErrorMessage = "Пріоритет обов'язковий")]
        public byte CampaignPriority { get; set; }
        [Required(ErrorMessage = "Назва обов'язкова")]
        public string CampaignName { get; set; } = null!;
        [Required(ErrorMessage = "Опис обов'язковий")]
        public string CampaignDescription { get; set; } = null!;
        [Required(ErrorMessage = "Перелік потреб обов'язковий")]
        public string ApplianceDescription { get; set; } = null!;
        [Required(ErrorMessage = "Фото обов'язкове")]

        [FromForm(Name = "CampaignPhoto")]
        public IFormFile? CampaignPhoto { get; set; }
        public string? CampaignPhotoBase64 { get; set; }
        [Required(ErrorMessage = "Необхідна сума обов'язкова")]
        public decimal CampaignGoal { get; set; }
        public decimal? Accumulated { get; set; }
        [Required(ErrorMessage = "Дата завершення обов'язкова")]
        public DateTime FinishDate { get; set; }
        public string ? FinishDateString { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ?CreateDateString { get; set; }
    }
}
