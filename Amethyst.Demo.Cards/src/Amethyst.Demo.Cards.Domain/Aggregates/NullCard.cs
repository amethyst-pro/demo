using Amethyst.Demo.Cards.Domain.Contracts;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Domain.ValueTypes;

namespace Amethyst.Demo.Cards.Domain.Aggregates
{
    public class NullCard : ICard
    {
        public bool IsRemoved => true;

        public long? CurrentVersion => default;

        public bool HasChanges => default;

        public CardSnapshot GetSnapshot()
            => CardSnapshot.Remeved;

        public void Rename(CardName name)
        {
        }

        public void Remove()
        {
        }

        public void Deposit(Money sum)
        {
        }

        public void Withdrawal(Money sum)
        {
        }
    }
}