using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Application.Commands
{
    public readonly struct RenameCard
    {
        public RenameCard(CardId cardId, CardName name)
        {
            CardId = cardId;
            Name = name;
        }

        public CardId CardId { get; }
        
        public CardName Name { get; }
    }
}