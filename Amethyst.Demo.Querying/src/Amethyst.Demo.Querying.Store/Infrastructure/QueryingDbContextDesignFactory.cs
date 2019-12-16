using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Amethyst.Demo.Querying.Store.Infrastructure
{
    public class QueryingDbContextDesignFactory: IDesignTimeDbContextFactory<QueryingDbContext>
    {
        public const string MigrationsHistoryTable = "__ef_migrations_history";
        
        public QueryingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QueryingDbContext>();
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=querying;Username=postgres;Password=postgres;",
                y => y.MigrationsHistoryTable(MigrationsHistoryTable));

            return new QueryingDbContext(optionsBuilder.Options);
        }
    }
}