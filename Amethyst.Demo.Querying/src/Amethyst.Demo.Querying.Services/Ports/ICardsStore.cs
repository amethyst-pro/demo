using System;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Models.Entities;

namespace Amethyst.Demo.Querying.Services.Ports
{
    public interface ICardsStore
    {
        Task<Card> GetAsync(Guid cardId);

        Task AddAsync(Card card);

        Task UpdateAsync(Card card);

        Task<bool> ExistsAsync(Guid cardId);
    }
}