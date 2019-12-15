using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Host.Serializers;
using Amethyst.EventStore.Hosting;
using Amethyst.EventStore.Hosting.Settings;
using Amethyst.EventStore.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Amethyst.Demo.Cards.Host.Composition
{
    public static class EventStoreExtensions
    {
        public static IServiceCollection ConfigureEventStore(
            this IServiceCollection services,
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            var con = configuration.GetSection("Postgres").Get<string>();
            var connections = new DbConnections(con, con);

            var kafkaBrokers = configuration.GetSection("Kafka:Brokers").Get<string>();
            
            const string serviceName = "cards";
            var topic = $"amethyst_{serviceName}_{typeof(Card).Name.ToLowerInvariant()}";

            var publisherFactory = new PublisherFactory(
                new RecordedEventSerializer(),
                new PublisherConfiguration(kafkaBrokers),
                loggerFactory);
            
            var settings = new AggregateSettingsBuilder()
                .AddAggregate<Card, CardId, CardFactory>(services, topic, 5)
                .Build(connections, publisherFactory, new EventSerializer());

            return services.AddEventStore(settings, loggerFactory);
        }
    }
}