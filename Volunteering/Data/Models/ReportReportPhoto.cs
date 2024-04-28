using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class ReportReportPhoto
    {
        public int ReportReportPhotoId { get; set; }
        public int? ReportId { get; set; }
        public int? ReportPhotoId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Report? Report { get; set; }
        public virtual ReportPhoto? ReportPhoto { get; set; }
    }
}
