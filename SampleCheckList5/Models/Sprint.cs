namespace SampleCheckList5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Sprint : DbContext
    {
        public Sprint()
            : base("name=Sprint")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.Items_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.CheckList_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.CheckList_Owner)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.Project_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
