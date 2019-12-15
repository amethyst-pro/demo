using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Contracts.Repositories;
using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Domain.Contracts;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Amethyst.Demo.Cards.Application.Ports.Repositories
{
    public sealed class CardRepository : ICardRepository
    {
        private readonly IRepository<Card, CardId> _repository;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(
            IRepository<Card, CardId> repository, 
            ILogger<CardRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public async Task<ICard> GetOrCreate(CardId cardId)
        {
            var card = await _repository.GetAsync(cardId);
            if (card.Any())
                return card.Single();
            
            _logger.LogDebug($"Get removed card {cardId}");
            
            return new NullCard();
        }

        public async Task SaveAsync(ICard card)
        {
            if (card is Card aggregate)
                await _repository.SaveAsync(aggregate);
        }
    }
}