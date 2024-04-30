using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class News
    {
        public Guid NewsId { get; set; }
        public Guid? UserId { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsText { get; set; } = null!;
        public byte[]? NewsPhoto { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual User? User { get; set; }
    }
}
