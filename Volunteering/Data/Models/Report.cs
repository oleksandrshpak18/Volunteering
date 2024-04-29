using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Report
    {
        public Report()
        {
            Campaigns = new HashSet<Campaign>();
            ReportReportPhotos = new HashSet<ReportReportPhoto>();
        }

        public Guid ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? ReportDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<ReportReportPhoto> ReportReportPhotos { get; set; }
    }
}
