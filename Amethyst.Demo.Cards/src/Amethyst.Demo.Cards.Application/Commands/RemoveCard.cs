using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Application.Commands
{
    public readonly struct RemoveCard
    {
        public RemoveCard(CardId cardId)
            => CardId = cardId;

        public CardId CardId { get; }
    }
}