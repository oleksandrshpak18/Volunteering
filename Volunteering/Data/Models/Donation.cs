using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Donation
    {
        public int DonationId { get; set; }
        public int? UserId { get; set; }
        public int? CampaignId { get; set; }
        public decimal? DonationValue { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Campaign? Campaign { get; set; }
        public virtual User? User { get; set; }
    }
}
