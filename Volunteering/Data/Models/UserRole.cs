using System;
using System.Collections.Generic;

namespace Volunteering.Data.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public Guid UserRoleId { get; set; }
        public string UserRoleName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
