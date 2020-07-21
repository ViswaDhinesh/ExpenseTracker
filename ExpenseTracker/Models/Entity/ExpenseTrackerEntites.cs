namespace ExpenseTracker
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExpenseTrackerEntites : DbContext
    {
        public ExpenseTrackerEntites()
            : base("name=ExpenseTrackerEntites")
        {
        }

        public virtual DbSet<ETExpenseMaster> ETExpenseMasters { get; set; }
        public virtual DbSet<ETMenu> ETMenus { get; set; }
        public virtual DbSet<ETMenuAccess> ETMenuAccesses { get; set; }
        public virtual DbSet<ETSource> ETSources { get; set; }
        public virtual DbSet<ETSubMenu> ETSubMenus { get; set; }
        public virtual DbSet<ETUser> ETUsers { get; set; }
        public virtual DbSet<ETCategory> ETCategories { get; set; }
        public virtual DbSet<ETValue> ETValues { get; set; }
        public virtual DbSet<ETRole> ETRoles { get; set; }
        public virtual DbSet<ETUserLog> ETUserLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ETSource>()
                .Property(e => e.SourceType)
                .IsUnicode(false);
        }
    }
}
