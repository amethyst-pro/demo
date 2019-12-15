using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Host.Serializers;
using Amethyst.EventStore.Abstractions;
using Amethyst.EventStore.Streams;
using Amethyst.EventStore.Streams.Metadata;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Amethyst.Demo.Cards.Host.Controllers
{
    [Route("api/insights/cards/{cardId}")]
    [ApiController]
    public class InsightsController : ControllerBase
    {
        private const string Category = "Card";
        private readonly IEventStore _store;

        private static readonly Dictionary<string, string> ShortNames =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"card", nameof(Card)},
            };

        public InsightsController(IEventStore store)
        {
            _store = store;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(Guid cardId)
        {
            var resolvedCategory = ShortNames.GetValueOrDefault(Category, Category);

            var streamId = new StreamId(resolvedCategory, cardId);
            
            var stream = new Stream(
                _store, 
                new EventSerializer(), 
                streamId, 
                NullMetadataContext.Instance);

            var eventsResult = await stream.ReadEventsForward();

            var result = eventsResult.Events.Select((e, i) => new
            {
                Num = i,
                e.StreamId,
                Event = JsonConvert.SerializeObject(e.Data),
            });

            return Ok(result);
        }
        
        [HttpGet("raw")]
        public async Task<IActionResult> GetRawEvents(Guid cardId)
        {
            var serializer = new MetadataSerializer();
            
            var resolvedCategory = ShortNames.GetValueOrDefault(Category, Category);

            var eventsResult = await _store.ReadStreamEventsForwardAsync(new StreamId(resolvedCategory, cardId), 0);

            return Ok(eventsResult.Events.Select(p => new
            {
                p.Id, 
                p.Type, 
                p.Number, 
                p.StreamId,
                p.Created,
                p.Data,
                Metadata = serializer.Deserialize(p.Metadata)
            }));
        }
    }
}