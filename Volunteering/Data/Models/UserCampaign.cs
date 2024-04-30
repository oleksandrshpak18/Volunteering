using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class UserCampaign
    {
        public Guid UserCampaignId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CampaignId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Campaign? Campaign { get; set; }
        public virtual User? User { get; set; }
    }
}
