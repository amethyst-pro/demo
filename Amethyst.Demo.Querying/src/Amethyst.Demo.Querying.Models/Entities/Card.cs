using System;
using Amethyst.Demo.Querying.Models.Primitives;

namespace Amethyst.Demo.Querying.Models.Entities
{
    public class Card
    {
        public Card(
            Guid cardId, 
            Guid userId, 
            string name,
            decimal balanceAmount, 
            Currency balanceCurrency, 
            bool isRemoved,
            DateTime createdAt)
        {
            CardId = cardId;
            UserId = userId;
            Name = name;
            BalanceAmount = balanceAmount;
            BalanceCurrency = balanceCurrency;
            IsRemoved = isRemoved;
            CreatedAt = createdAt;
        }

        public Guid CardId { get; }
        
        public Guid UserId { get; }
        
        public string Name { get; private set; }

        public decimal BalanceAmount { get; private set; }
        
        public Currency BalanceCurrency { get; private set; }
        
        public bool IsRemoved { get; private set; }
        
        public DateTime? LastModified { get; private set; }

        public DateTime CreatedAt { get; }

        public void Rename(string name)
            => Name = name;

        public void ChangeBalance(decimal balanceAmount, Currency balanceCurrency)
        {
            BalanceAmount = balanceAmount;
            BalanceCurrency = balanceCurrency;
        }

        public void Remove()
        {
            IsRemoved = true;
        }
        public void ChangeTimestamp(DateTime lastModified)
        {
            LastModified = lastModified;
        }
    }
}