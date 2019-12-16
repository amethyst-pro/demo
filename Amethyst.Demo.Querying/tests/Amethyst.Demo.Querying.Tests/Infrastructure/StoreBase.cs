using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Store.Infrastructure;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using SharpJuice.AutoFixture;

namespace Amethyst.Demo.Querying.Tests.Infrastructure
{
    public abstract class StoreBase : IDisposable
    {
        protected StoreBase()
        {
            var configuration = new PostgresConfiguration();
            _database = configuration.CreateDatabase();
            Context = CreateContext();
            Context.Database.Migrate();
            Fixture.Register(() => DateTimeOffset.UtcNow);
        }
        
        protected QueryingDbContext Context { get; }
        protected IFixture Fixture { get; } = new Fixture();

        private readonly TempDatabase _database;

        protected QueryingDbContext CreateContext()
        {
            return new QueryingDbContext(new DbContextOptionsBuilder<QueryingDbContext>()
                .UseNpgsql(_database.ConnectionString)
                .Options);
        }

        public void Dispose()
        {
            Context.Dispose();
            _database.Dispose();
        }

        protected Task ReloadEntities<T>(IEnumerable<T> entities)
            where T : class
            => Task.WhenAll(entities.Select(x => Context.Entry(x).ReloadAsync()));
    }
}