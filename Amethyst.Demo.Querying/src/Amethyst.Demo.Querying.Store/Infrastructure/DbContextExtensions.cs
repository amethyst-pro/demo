using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amethyst.Demo.Querying.Store.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Amethyst.Demo.Querying.Store.Infrastructure
{
    public static class DbContextExtensions
    {
        public static bool IsAttached<T>(this DbContext context, T entity)
            where T : class
            => context.Set<T>().Local.Contains(entity);

        public static IEnumerable<Expression<Func<T, object>>> GetPropertiesForUpdate<T>(this DbContext context)
        {
            var properties = context.Model.FindEntityType(typeof(T))
                .GetProperties()
                .Where(p => !p.IsPrimaryKey() && 
                            !p.IsConcurrencyToken && 
                            p.PropertyInfo != null && 
                            p.CanUpdate())
                .Select(p => p.PropertyInfo);

            var entity = Expression.Parameter(typeof(T));

            foreach (var propertyInfo in properties)
            {
                var instance = Expression.Parameter(propertyInfo.DeclaringType ?? throw new InvalidOperationException(), "i");
                var property = Expression.Property(instance, propertyInfo);
                var convert = Expression.TypeAs(property, typeof(object));

                yield return (Expression<Func<T, object>>) Expression.Lambda(convert, entity);
            }
        }
    }
}