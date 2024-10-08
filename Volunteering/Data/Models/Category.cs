﻿using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            CategorySubcategories = new HashSet<CategorySubcategory>();
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<CategorySubcategory> CategorySubcategories { get; set; }
    }
}
