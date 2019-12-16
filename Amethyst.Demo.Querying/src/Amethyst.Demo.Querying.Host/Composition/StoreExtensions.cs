using Amethyst.Demo.Querying.Services.Ports;
using Amethyst.Demo.Querying.Store;
using Microsoft.Extensions.DependencyInjection;

namespace Amethyst.Demo.Querying.Host.Composition
{
    public static class StoreExtensions
    {
        public static IServiceCollection AddStores(this IServiceCollection services)
        {
            services.AddTransient<ICardsStore, CardsStore>();

            return services;
        } 
    }
}