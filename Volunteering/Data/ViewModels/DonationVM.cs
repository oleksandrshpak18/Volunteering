using System.ComponentModel.DataAnnotations;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class DonationVM
    {
        public Guid ? donationId { get; set; }
        public Guid? UserId { get; set; }

        public string? CampaignName { get; set; }
        public Guid? CampaignId { get; set; }
        [Required(ErrorMessage ="Сума донату обов'язкова для заповнення")]
        [Range(0.01, 999_999.99, ErrorMessage = "Необхідна сума повинна бути більше 0 і менше 1,000,000 (1 млн грн)")]
        public decimal? DonationValue { get; set; }

        public string? DonationDate { get; set; }
    }
}
