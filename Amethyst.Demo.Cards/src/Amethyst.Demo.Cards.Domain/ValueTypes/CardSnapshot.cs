using System;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;

namespace Amethyst.Demo.Cards.Domain.ValueTypes
{
    public readonly struct CardSnapshot : IEquatable<CardSnapshot>
    {
        public CardSnapshot(
            CardId id, 
            UserId userId,
            CardName name,
            Money balance, 
            bool isRemoved)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Balance = balance;
            IsRemoved = isRemoved;
        }

        public CardId Id { get; }
        
        public UserId UserId { get; }
        
        public CardName Name { get; }
        
        public Money Balance { get; }
        
        public bool IsRemoved { get; }
        
        public static CardSnapshot Remeved 
            => new CardSnapshot(
                default,
                default, 
                default,
                default, 
                true);

        public bool Equals(CardSnapshot other)
            => Id.Equals(other.Id) 
               && UserId.Equals(other.UserId) 
               && Name.Equals(other.Name)
               && Balance.Equals(other.Balance) 
               && IsRemoved == other.IsRemoved;

        public override bool Equals(object obj)
            => obj is CardSnapshot other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Balance.GetHashCode();
                hashCode = (hashCode * 397) ^ IsRemoved.GetHashCode();
                return hashCode;
            }
        }
    }
}