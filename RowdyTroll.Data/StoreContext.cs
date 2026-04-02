using Microsoft.EntityFrameworkCore;
using RowdyTroll.Domain.Catalog;

namespace RowdyTroll.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<RowdyTroll.Domain.Orders.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbInitializer.Initialize(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
