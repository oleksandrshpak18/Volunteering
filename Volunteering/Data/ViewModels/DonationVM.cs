using System.ComponentModel.DataAnnotations;
using Volunteering.Data.Models;

namespace Volunteering.Data.ViewModels
{
    public class DonationVM
    {
        public Guid ? donationId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CampaignId { get; set; }
        [Required]
        public decimal? DonationValue { get; set; }

    }
}
