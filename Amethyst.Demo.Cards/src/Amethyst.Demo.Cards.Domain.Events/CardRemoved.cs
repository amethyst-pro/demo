using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Events
{
    public sealed class CardRemoved : IDomainEvent
    {
        public CardRemoved(CardId cardId, UserId userId)
        {
            CardId = cardId;
            UserId = userId;
        }

        public CardId CardId { get; }
        
        public UserId UserId { get; }
    }
}