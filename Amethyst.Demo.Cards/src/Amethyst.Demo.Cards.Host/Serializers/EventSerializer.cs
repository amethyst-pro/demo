using System;
using System.Text;
using Amethyst.EventStore.Abstractions;
using Amethyst.EventStore.Abstractions.Metadata;
using Amethyst.EventStore.Abstractions.Serialization;
using Newtonsoft.Json;

namespace Amethyst.Demo.Cards.Host.Serializers
{
    // Demo serializer example. Don't use in production!
    public sealed class EventSerializer : IEventSerializer
    {
        public object Deserialize(RecordedEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
            
            if (@event.Data == null)
                throw new ArgumentException("data not specified");

            var json = Encoding.UTF8.GetString(@event.Data);
            var type = Type.GetType(@event.Type);

            return JsonConvert.DeserializeObject(json, type);
        }

        public EventData Serialize(object @event, Guid eventId, EventMetadata? metadata = null)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
            
            var name = @event.GetType().AssemblyQualifiedName;
            var json = JsonConvert.SerializeObject(@event);
            var bytes = Encoding.UTF8.GetBytes(json);

            return new EventData(eventId, name , bytes, default );
        }

        public bool HasKnownType(RecordedEvent @event) => true;
    }
}