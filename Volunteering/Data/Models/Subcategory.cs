using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Campaigns = new HashSet<Campaign>();
            CategorySubcategories = new HashSet<CategorySubcategory>();
        }

        public Guid SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<CategorySubcategory> CategorySubcategories { get; set; }
    }
}
