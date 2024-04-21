using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsText { get; set; } = null!;
        public byte[]? NewsPhoto { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
