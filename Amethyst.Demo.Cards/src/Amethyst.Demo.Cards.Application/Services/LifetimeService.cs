using System.Linq;
using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Commands;
using Amethyst.Demo.Cards.Application.Contracts.Services;
using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Amethyst.Demo.Cards.Application.Services
{
    public sealed class LifetimeService : ILifetimeService
    {
        private readonly IRepository<Card, CardId> _repository;
        private readonly ILogger<LifetimeService> _logger;

        public LifetimeService(
            IRepository<Card, CardId> repository, 
            ILogger<LifetimeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateAsync(CreateCard command)
        {
            var card = (await _repository.GetAsync(command.CardId)).SingleOrDefault();
            if (card != null)
                return;

            var newCard = new Card(command.CardId, command.UserId, command.Name);

            await _repository.SaveAsync(newCard);
            
            _logger.LogDebug($"Card created ({command.CardId}) for user {command.UserId}.");
        }

        public async Task RemoveAsync(RemoveCard command)
        {
            var card = (await _repository.GetAsync(command.CardId)).SingleOrDefault();
            if (card == null || card.IsRemoved)
                return;
            
            card.Remove();

            await _repository.SaveAsync(card);
            
            _logger.LogDebug($"Card removed ({command.CardId}).");
        }
    }
}