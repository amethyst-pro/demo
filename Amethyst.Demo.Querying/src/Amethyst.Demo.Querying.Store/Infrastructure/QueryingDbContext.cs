using Amethyst.Demo.Querying.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Amethyst.Demo.Querying.Store.Infrastructure
{
    public class QueryingDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public QueryingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}