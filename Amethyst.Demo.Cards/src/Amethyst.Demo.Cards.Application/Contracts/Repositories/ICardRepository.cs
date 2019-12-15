using System.Threading.Tasks;
using Amethyst.Demo.Cards.Domain.Contracts;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Application.Contracts.Repositories
{
    public interface ICardRepository
    {
        Task<ICard> GetOrCreate(CardId cardId);

        Task SaveAsync(ICard card);
    }
}