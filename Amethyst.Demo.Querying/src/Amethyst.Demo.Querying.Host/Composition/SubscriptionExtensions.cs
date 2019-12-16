using System;
using Amethyst.Demo.Querying.Host.Serializers;
using Amethyst.Subscription.Broker;
using Amethyst.Subscription.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amethyst.Demo.Querying.Host.Composition
{
    public static class SubscriptionExtensions
    {
        public static IServiceCollection AddSubscription(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            const string groupId = "amethyst_querying";

            var kafkaBrokers = configuration.GetSection("Kafka:Brokers").Get<string>();
            var consumerSettings = configuration.GetSection("CardsConsumer").Get<ConsumerSettings>();

            consumerSettings.Config.GroupId = groupId;
            consumerSettings.Config.BootstrapServers = kafkaBrokers;

            var endpointConfig = new EndpointConfiguration();
            
            endpointConfig.AddSubscription(
            consumerSettings, 
            new DomainEventDeserializer()
            );
            
            services.AddSubscriptions(
                endpointConfig,
                ServiceLifetime.Scoped,
                AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}