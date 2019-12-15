using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Application.Commands
{
    public readonly struct DecreaseBalance
    {
        public DecreaseBalance(CardId cardId, Money sum)
        {
            CardId = cardId;
            Sum = sum;
        }

        public CardId CardId { get; }
        
        public Money Sum { get; }
    }
}