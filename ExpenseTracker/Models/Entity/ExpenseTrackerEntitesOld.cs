using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ExpenseTracker
{
    public partial class ExpenseTrackerEntitesOld : DbContext
    {
        public ExpenseTrackerEntitesOld()
            : base("name=ExpenseTrackerEntitesOld")
        {
        }

        public virtual DbSet<ETExpenseMaster> ETExpenseMasters { get; set; }
        public virtual DbSet<ETMenu> ETMenus { get; set; }
        public virtual DbSet<ETSubMenu> ETSubMenus { get; set; }
        public virtual DbSet<ETUser> ETUsers { get; set; }
        public virtual DbSet<ETMenuAccess> ETMenuAccesss { get; set; }
        public virtual DbSet<ETSource> ETSources { get; set; }
    }
}
