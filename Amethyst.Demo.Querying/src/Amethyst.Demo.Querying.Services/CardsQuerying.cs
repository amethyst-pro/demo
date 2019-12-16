using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Models.Entities;
using Amethyst.Demo.Querying.Services.Contracts;
using Amethyst.Demo.Querying.Services.Ports;

namespace Amethyst.Demo.Querying.Services
{
    public sealed class CardsQuerying : ICardQuerying
    {
        private readonly ICardsStore _cardsStore;
        
        public CardsQuerying(ICardsStore cardsStore)
        {
            _cardsStore = cardsStore;
        }

        public Task<IReadOnlyCollection<Card>> GetAsync()
        {
            return _cardsStore.GetAsync();
        }

        public async Task<Card> GetAsync(Guid cardId)
        {
            if (!await _cardsStore.ExistsAsync(cardId))
                return default;
            
            return await _cardsStore.GetAsync(cardId);
        }
    }
}