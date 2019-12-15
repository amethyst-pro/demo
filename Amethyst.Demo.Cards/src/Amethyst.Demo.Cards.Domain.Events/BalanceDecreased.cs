using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Events
{
    public sealed class BalanceDecreased : IDomainEvent
    {
        public BalanceDecreased(CardId cardId, UserId userId, Money balance)
        {
            CardId = cardId;
            UserId = userId;
            Balance = balance;
        }

        public CardId CardId { get; }
        
        public UserId UserId { get; }
        
        public Money Balance { get; }
    }
}