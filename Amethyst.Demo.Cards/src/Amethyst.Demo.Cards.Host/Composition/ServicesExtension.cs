using Amethyst.Demo.Cards.Application.Contracts.Services;
using Amethyst.Demo.Cards.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Amethyst.Demo.Cards.Host.Composition
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddTransient<ILifetimeService, LifetimeService>()
                .AddTransient<ICardService, CardService>();
    }
}