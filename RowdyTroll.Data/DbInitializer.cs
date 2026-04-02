using Microsoft.EntityFrameworkCore;
using RowdyTroll.Domain.Catalog;

namespace RowdyTroll.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ModelBuilder builder)
        {
            // Seed two items. Use anonymous objects so EF can set non-public setters.
            builder.Entity<Item>().HasData(
                new { Id = 1, Name = "Gadget", Description = "A useful gadget", Brand = "Acme", Price = 19.99m },
                new { Id = 2, Name = "Widget", Description = "A handy widget", Brand = "Contoso", Price = 29.99m }
            );
        }
    }
}
