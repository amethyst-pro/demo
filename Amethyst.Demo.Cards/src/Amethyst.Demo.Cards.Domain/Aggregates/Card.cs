using System.Collections.Generic;
using Amethyst.Demo.Cards.Domain.Contracts;
using Amethyst.Demo.Cards.Domain.Events;
using Amethyst.Demo.Cards.Domain.Events.Primitives;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Domain.ValueTypes;
using Amethyst.EventStore.Domain;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Aggregates
{
    public sealed class Card : AggregateBase<CardId>, ICard
    {
        private UserId _userId;
        private CardName _name;
        private bool _isRemoved;
        private Money _balance = new Money(0, Currency.Rub);

        public Card(CardId id, UserId userId, CardName name) 
            : base(id)
        {
            ApplyEvent(new CardCreated(id, userId, name));
        }

        public Card(CardId id, long version, IReadOnlyCollection<IDomainEvent> events) 
            : base(id, version)
        {
            ApplyCommittedEvents(events);
        }

        public bool IsRemoved => _isRemoved;

        public long? CurrentVersion => Version.OrDefault();

        bool ICard.HasChanges => HasChanges();

        public CardSnapshot GetSnapshot()
            => new CardSnapshot(Id, _userId, _name, _balance, _isRemoved);

        public void Rename(CardName name)
        {
            if (_name.Equals(name))
                return;
            
            ApplyEvent(new CardRenamed(Id, _userId, name));
        }

        public void Remove()
        {
            if (_isRemoved)
                return;
            
            ApplyEvent(new CardRemoved(Id, _userId));
        }

        public void Deposit(Money sum)
        {
            if (sum.Amount == 0)
                return;
            
            var newBalance = _balance += sum;
            ApplyEvent(new BalanceIncreased(Id, _userId, newBalance));
        }

        public void Withdrawal(Money sum)
        {
            if (sum.Amount == 0)
                return;
            
            if (_balance.Amount - sum.Amount < 0)
                return;

            var newBalance = _balance -= sum;
            ApplyEvent(new BalanceDecreased(Id, _userId, newBalance));
        }
        
        protected override void OnApplyEvent(IDomainEvent @event)
            => When((dynamic) @event);

        private void When(CardCreated @event)
        {
            _userId = @event.UserId;
            _name = @event.Name;
            _balance = default;
            _isRemoved = false;
        }
        
        private void When(CardRenamed @event)
        {
            _name = @event.Name;
        }
        
        private void When(BalanceIncreased @event)
        {
            _balance = @event.Balance;
        }
        
        private void When(BalanceDecreased @event)
        {
            _balance = @event.Balance;
        }

        private void When(CardRemoved @event)
        {
            _isRemoved = true;
        }
    }
}