using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Domain.ValueTypes;

namespace Amethyst.Demo.Cards.Domain.Contracts
{
    public interface ICard
    {
        bool IsRemoved { get; }
        
        long? CurrentVersion { get; }
        
        bool HasChanges { get; }

        CardSnapshot GetSnapshot();

        void Rename(CardName name);

        void Deposit(Money sum);

        void Withdrawal(Money sum);
        
        void Remove();
    }
}