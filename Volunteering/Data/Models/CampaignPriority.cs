using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class CampaignPriority
    {
        public CampaignPriority()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public Guid CampaignPriorityId { get; set; }
        public byte PriorityValue { get; set; }
        public string PriorityDescription { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
