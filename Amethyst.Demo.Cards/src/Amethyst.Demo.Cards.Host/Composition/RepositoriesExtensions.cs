using Amethyst.Demo.Cards.Application.Contracts.Repositories;
using Amethyst.Demo.Cards.Application.Ports.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Amethyst.Demo.Cards.Host.Composition
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICardRepository, CardRepository>();

            return services;
        }
    }
}