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
        [Range(0.01, 999_999.99, ErrorMessage = "Необхідна сума повинна бути більше 0 і менше 1,000,000 (1 млн грн)")]
        public decimal CampaignGoal { get; set; }
        public decimal? Accumulated { get; set; }
        [Required(ErrorMessage = "Дата завершення обов'язкова")]
        [DateLaterThanToday(ErrorMessage = "Дата завершення повинна бути пізніше ніж сьогодні")]
        public DateTime FinishDate { get; set; }
        public string ? FinishDateString { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ?CreateDateString { get; set; }
    }

    public class DateLaterThanTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime > DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Дата завершення повинна бути пізніше ніж сьогодні");
                }
            }
            return new ValidationResult("Неправильний формат дати");
        }
    }
}


