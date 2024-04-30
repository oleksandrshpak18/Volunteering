using System.ComponentModel.DataAnnotations;

namespace Volunteering.Data.ViewModels
{
    public class CampaignStatusVm
    {
        public Guid? CampaignStatusId { get; set; }
        [Required]
        public string StatusName { get; set; } = null!;
        [Required]
        public string StatusDescription { get; set; } = null!;
    }
}
