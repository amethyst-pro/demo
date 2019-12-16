using Amethyst.Demo.Querying.Store.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Amethyst.Demo.Querying.Host.Composition
{
    public static class ModelExtensions
    {
        public static IServiceCollection AddPricingReadModel(
            this IServiceCollection collection, 
            string connectionString)
        {
            collection.AddEntityFrameworkNpgsql()
                .AddDbContext<QueryingDbContext>(x => x
                    .UseNpgsql(connectionString, y => y.MigrationsHistoryTable(QueryingDbContextDesignFactory.MigrationsHistoryTable)));

            using (var context = collection.BuildServiceProvider().GetRequiredService<QueryingDbContext>())
                context.Database.Migrate();

            return collection;
        }
    }
}