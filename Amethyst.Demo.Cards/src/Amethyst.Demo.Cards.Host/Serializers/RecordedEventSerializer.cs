using System.Text;
using Amethyst.EventStore.Abstractions;
using Amethyst.EventStore.Abstractions.Serialization;
using Newtonsoft.Json;

namespace Amethyst.Demo.Cards.Host.Serializers
{
    // Demo serializer example. Don't use in production!
    public sealed class RecordedEventSerializer : IRecordedEventSerializer
    {
        public byte[] Serialize(RecordedEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}