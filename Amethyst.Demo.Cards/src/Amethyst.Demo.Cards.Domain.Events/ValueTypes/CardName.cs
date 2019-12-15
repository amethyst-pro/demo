using System;

namespace Amethyst.Demo.Cards.Domain.Events.ValueTypes
{
    public readonly struct CardName : IEquatable<CardName>
    {
        public CardName(string value)
            => Value = value;

        public string Value { get; }

        public bool Equals(CardName other)
            => string.Equals(Value, other.Value);

        public override bool Equals(object obj)
            => obj is CardName other && Equals(other);

        public override int GetHashCode()
            => Value != null ? Value.GetHashCode() : 0;
    }
}