using System;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Events.ValueTypes
{
    public readonly struct CardId : IGuidId, IEquatable<CardId>
    {
        public CardId(Guid value)
            =>  Value = value;

        public Guid Value { get; }

        public bool Equals(CardId other)
            => Value.Equals(other.Value);

        public override bool Equals(object obj)
            => obj is CardId other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value.ToString();
    }
}