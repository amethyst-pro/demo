using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amethyst.Demo.Querying.Store.Configuration
{
    public static class PropertyBuilderExtensions
    {
        private static readonly object DoNotUpdateAnnotation = new object();

        private const int NameMaxLength = 50;

        public static PropertyBuilder<string> IsName(this PropertyBuilder<string> builder)
            => builder.HasMaxLength(NameMaxLength);
        
        public static bool CanUpdate(this IProperty property) =>
            property.FindAnnotation(nameof(DoNotUpdateAnnotation)) == null;
    }
}