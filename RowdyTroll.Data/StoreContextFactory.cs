using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RowdyTroll.Data
{
    // Design-time factory for StoreContext so tools (migrations / scaffolding) can create the context
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlite("Data Source=../Registrar.sqlite",
                b => b.MigrationsAssembly("RowdyTroll.Api"));

            return new StoreContext(optionsBuilder.Options);
        }
    }
}
