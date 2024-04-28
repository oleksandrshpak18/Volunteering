using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Donations = new HashSet<Donation>();
            Payoffs = new HashSet<Payoff>();
            UserCampaigns = new HashSet<UserCampaign>();
        }

        public int CampaignId { get; set; }
        public int? ReportId { get; set; }
        public int? CategoryId { get; set; }
        public int? CampaignStatusId { get; set; }
        public int? CampaignPriorityId { get; set; }
        public string CampaignName { get; set; } = null!;
        public string CampaignDescription { get; set; } = null!;
        public string ApplianceDescription { get; set; } = null!;
        public byte[]? CampaignPhoto { get; set; }
        public decimal CampaignGoal { get; set; }
        public decimal? Accumulated { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual CampaignPriority? CampaignPriority { get; set; }
        public virtual CampaignStatus? CampaignStatus { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Report? Report { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Payoff> Payoffs { get; set; }
        public virtual ICollection<UserCampaign> UserCampaigns { get; set; }
    }
}
