using System.Linq;
using System.Text;
using Amethyst.EventStore.Abstractions.Metadata;
using Newtonsoft.Json;

namespace Amethyst.Demo.Cards.Host.Serializers
{
    // Demo serializer example. Don't use in production!
    public sealed class MetadataSerializer : IMetadataSerializer
    {
        public byte[] Serialize(EventMetadata meta)
        {
            var json = JsonConvert.SerializeObject(meta);

            return Encoding.UTF8.GetBytes(json);
        }

        public EventMetadata Deserialize(byte[] bytes)
        {
            if (bytes == null || !bytes.Any())
                return default;
            
            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<EventMetadata>(json);
        }
    }
}