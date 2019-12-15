using System;
using System.Threading.Tasks;
using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Domain.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Amethyst.Demo.Cards.Host.Controllers
{
    [Route("api/snapshot/cards/{cardId}")]
    [ApiController]
    public sealed class SnapshotController : ControllerBase
    {
        private readonly IRepository<Card, CardId> _repository;

        public SnapshotController(IRepository<Card, CardId> repository)
            =>  _repository = repository;

        [HttpGet]
        public async Task<ActionResult<CardSnapshot>> GetEvents(Guid cardId)
        {
            var id = new CardId(cardId);
            
            var card = await _repository.GetAsync(id);

            if (card.Any())
                return card.Single().GetSnapshot();

            return NoContent();
        }
    }
}