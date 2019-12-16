using Amethyst.Demo.Querying.Services;
using Amethyst.Demo.Querying.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using SharpJuice.Essentials;

namespace Amethyst.Demo.Querying.Host.Composition
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICardQuerying, CardsQuerying>();
            services.AddSingleton<IClock, Clock>();

            return services;
        }
    }
}