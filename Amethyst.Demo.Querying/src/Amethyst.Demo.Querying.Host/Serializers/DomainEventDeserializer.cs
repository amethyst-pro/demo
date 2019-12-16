using System;
using System.Text;
using Amethyst.EventStore.Abstractions;
using Amethyst.Subscription.Abstractions;
using Amethyst.Subscription.Serializers;
using Newtonsoft.Json;

namespace Amethyst.Demo.Querying.Host.Serializers
{
    // Demo serializer example. Don't use in production!
    public sealed class DomainEventDeserializer : IEventDeserializer
    {
        public IStreamEvent Deserialize(ReadOnlySpan<byte> message)
        {
            var rawEvent = Encoding.UTF8.GetString(message);
            var recordedEvent = JsonConvert.DeserializeObject<RecordedEvent>(rawEvent);

            var json = Encoding.UTF8.GetString(recordedEvent.Data);
            var type = Type.GetType(recordedEvent.Type);

            var domainEvent = JsonConvert.DeserializeObject(json, type);
            return new StreamEvent(domainEvent);
        }
    }
}