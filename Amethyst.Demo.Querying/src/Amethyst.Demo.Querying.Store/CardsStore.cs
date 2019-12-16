using System;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Models.Entities;
using Amethyst.Demo.Querying.Services.Ports;
using Amethyst.Demo.Querying.Store.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Amethyst.Demo.Querying.Store
{
    public sealed class CardsStore : ICardsStore
    {
        private readonly QueryingDbContext _context;

        public CardsStore(QueryingDbContext context)
            => _context = context;

        public Task<Card> GetAsync(Guid cardId)
            => _context.Cards.FirstAsync(x => x.CardId == cardId);
        
        public Task AddAsync(Card card)
        {
            _context.Cards.Add(card);

            return _context.SaveChangesAsync();
        }
        
        public Task UpdateAsync(Card card)
        {
            _context.Update(card);

            return _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid cardId)
            => _context.Cards.AnyAsync(p => p.CardId == cardId);
    }
}