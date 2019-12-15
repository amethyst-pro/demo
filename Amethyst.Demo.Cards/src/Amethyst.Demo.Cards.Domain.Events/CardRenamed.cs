using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Events
{
    public sealed class CardRenamed : IDomainEvent
    {
        public CardRenamed(CardId cardId, UserId userId, CardName name)
        {
            CardId = cardId;
            UserId = userId;
            Name = name;
        }

        public CardId CardId { get; }
        
        public UserId UserId { get; }
        
        public CardName Name { get; }
    }
}