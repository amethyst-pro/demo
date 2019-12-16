using System.Threading.Tasks;
using Amethyst.Demo.Cards.Domain.Events;
using Amethyst.Demo.Querying.Models.Entities;
using Amethyst.Demo.Querying.Models.Primitives;
using Amethyst.Demo.Querying.Services.Ports;
using Amethyst.Subscription.Abstractions;
using Microsoft.Extensions.Logging;
using SharpJuice.Essentials;

namespace Amethyst.Demo.Querying.Services
{
    public sealed class CardsService : 
        IEventHandler<CardCreated>,
        IEventHandler<CardRemoved>,
        IEventHandler<CardRenamed>,
        IEventHandler<BalanceIncreased>,
        IEventHandler<BalanceDecreased>
    {
        private readonly ICardsStore _cardsStore;
        private readonly ILogger<CardsService> _logger;
        private readonly IClock _clock;

        public CardsService(
            ICardsStore cardsStore,
            ILogger<CardsService> logger, 
            IClock clock)
        {
            _cardsStore = cardsStore;
            _logger = logger;
            _clock = clock;
        }

        public async Task HandleAsync(CardCreated @event)
        {
            if (await _cardsStore.ExistsAsync(@event.UserId.Value))
                return;

            const bool isRemoved = false;
            const decimal defaultBalance = 0;
            
            var card = new Card(
                @event.CardId.Value,
                @event.UserId.Value, 
                @event.Name.Value,
                defaultBalance,
                Currency.Rub,
                isRemoved,
                _clock.Now.Date);

            await _cardsStore.AddAsync(card);
 
            _logger.LogDebug($"Card {@event.UserId} created.");
        }

        public async Task HandleAsync(CardRemoved @event)
        {
            if (!await _cardsStore.ExistsAsync(@event.CardId.Value))
                return;

            var card = await _cardsStore.GetAsync(@event.CardId.Value);
            
            card.Remove();
            card.ChangeTimestamp(_clock.Now.DateTime);

            await _cardsStore.UpdateAsync(card);

            _logger.LogDebug($"Card {@event.UserId} removed.");
        }

        public async Task HandleAsync(CardRenamed @event)
        {
            if (!await _cardsStore.ExistsAsync(@event.CardId.Value))
                return;

            var card = await _cardsStore.GetAsync(@event.CardId.Value);
            
            card.Rename(@event.Name.Value);
            card.ChangeTimestamp(_clock.Now.DateTime);

            await _cardsStore.UpdateAsync(card);
  
            _logger.LogDebug($"Card {@event.UserId} renamed.");
        }
        
        public async Task HandleAsync(BalanceIncreased @event)
        {
            if (!await _cardsStore.ExistsAsync(@event.CardId.Value))
                return;

            var card = await _cardsStore.GetAsync(@event.CardId.Value);
            
            card.ChangeBalance(@event.Balance.Amount, (Currency)@event.Balance.Currency);
            card.ChangeTimestamp(_clock.Now.DateTime);

            await _cardsStore.UpdateAsync(card);

            _logger.LogDebug($"Card {@event.UserId} balance increased.");
        }

        public async Task HandleAsync(BalanceDecreased @event)
        {
            if (!await _cardsStore.ExistsAsync(@event.CardId.Value))
                return;

            var card = await _cardsStore.GetAsync(@event.CardId.Value);
            
            card.ChangeBalance(@event.Balance.Amount, (Currency)@event.Balance.Currency);
            card.ChangeTimestamp(_clock.Now.DateTime);

            await _cardsStore.UpdateAsync(card);

            _logger.LogDebug($"Card {@event.UserId} balance decreased.");
        }
    }
}