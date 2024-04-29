using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class CategorySubcategory
    {
        public Guid CategorySubcategoryId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
    }
}
