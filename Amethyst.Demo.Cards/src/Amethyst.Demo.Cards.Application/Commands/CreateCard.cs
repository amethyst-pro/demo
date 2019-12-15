using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Application.Commands
{
    public readonly struct CreateCard
    {
        public CreateCard(CardId cardId, UserId userId, CardName name)
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