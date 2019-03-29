namespace Coffee.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoffeDC : DbContext
    {
        public CoffeDC()
            : base("name=CoffeDC")
        {
        }

        public virtual DbSet<CoffeCount> CoffeCounts { get; set; }
        public virtual DbSet<CoffeLover> CoffeLovers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoffeLover>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CoffeLover>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<CoffeLover>()
                .HasMany(e => e.CoffeCounts)
                .WithRequired(e => e.CoffeLover)
                .WillCascadeOnDelete(false);
        }
    }
}
