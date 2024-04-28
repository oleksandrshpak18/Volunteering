using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class ReportPhoto
    {
        public ReportPhoto()
        {
            ReportReportPhotos = new HashSet<ReportReportPhoto>();
        }

        public int ReportPhotoId { get; set; }
        public byte[] Photo { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<ReportReportPhoto> ReportReportPhotos { get; set; }
    }
}
