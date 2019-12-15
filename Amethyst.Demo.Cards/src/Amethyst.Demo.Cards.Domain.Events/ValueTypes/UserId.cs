using System;
using Amethyst.EventStore.Domain.Abstractions;

namespace Amethyst.Demo.Cards.Domain.Events.ValueTypes
{
    public readonly struct UserId : IGuidId, IEquatable<UserId>
    {
        public UserId(Guid value)
            =>  Value = value;

        public Guid Value { get; }

        public bool Equals(UserId other)
            => Value.Equals(other.Value);

        public override bool Equals(object obj)
            => obj is UserId other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value.ToString();
    }
}