using Amethyst.Demo.Querying.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amethyst.Demo.Querying.Store.Configuration
{
    public sealed class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(x => x.CardId)
                .ValueGeneratedNever();
            
            builder.Property(x => x.UserId)
                .ValueGeneratedNever();

            builder.Property(x => x.CreatedAt)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsName();
            
            builder.UseXminAsConcurrencyToken()
                .HasKey(x => x.CardId);
        }
    }
}