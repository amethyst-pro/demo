using System.Collections.Generic;
using Amethyst.Demo.Cards.Domain.Aggregates;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Host.Serializers
{
    public sealed class CardFactory : IAggregateFactory<Card, CardId>
    {
        public Card Create(CardId id, long version, IReadOnlyCollection<IDomainEvent> events)
            => new Card(id, version, events);
    }
}