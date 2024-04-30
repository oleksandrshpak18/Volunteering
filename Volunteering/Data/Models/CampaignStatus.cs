using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class CampaignStatus
    {
        public CampaignStatus()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public Guid CampaignStatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
