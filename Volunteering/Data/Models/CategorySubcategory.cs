using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class CategorySubcategory
    {
        public int CategorySubcategoryId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
    }
}
