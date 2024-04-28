using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Payoff
    {
        public int PayoffId { get; set; }
        public int CampaignId { get; set; }
        public string RecipientName { get; set; } = null!;
        public string RecipientSurname { get; set; } = null!;
        public string RecippientCardNumber { get; set; } = null!;
        public decimal PayoffValue { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
    }
}
