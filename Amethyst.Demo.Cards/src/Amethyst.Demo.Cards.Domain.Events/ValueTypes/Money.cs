using System;
using Amethyst.Demo.Cards.Domain.Events.Primitives;

namespace Amethyst.Demo.Cards.Domain.Events.ValueTypes
{
  public readonly struct Money
  {
    private readonly Currency _currency;
    
    public Money(decimal amount, Currency currency)
    {
      if (amount < decimal.Zero)
        throw new ArgumentOutOfRangeException(nameof(amount), 
          $"Amount ({amount}) must be positive or zero.");
      
      Amount = amount;
      _currency = currency;
    }

    public decimal Amount { get; }

    public Currency Currency => _currency == 0
      ? Currency.Rub
      : _currency;

    public static bool operator ==(Money a, Money b)
    {
      Money.ValidateCurrencies(a, b);
      return a.Amount == b.Amount;
    }

    public static bool operator !=(Money a, Money b)
      => !(a == b);

    public static bool operator >(Money a, Money b)
    {
      Money.ValidateCurrencies(a, b);
      return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
      Money.ValidateCurrencies(a, b);
      return a.Amount < b.Amount;
    }
    
    public static Money operator +(Money a, Money b)
    {
      Money.ValidateCurrencies(a, b);
      return new Money(Round(a.Amount + b.Amount), a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
      Money.ValidateCurrencies(a, b);
      return new Money(Round(a.Amount - b.Amount), a.Currency);
    }

    public bool Equals(Money other)
    {
      if (Amount == other.Amount)
        return Currency == other.Currency;
      
      return false;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is Money other))
        return false;
      
      return Equals(other);
    }

    public override int GetHashCode()
      => Amount.GetHashCode() * 397 ^ Currency.GetHashCode();
    
    private static void ValidateCurrencies(Money a, Money b)
    {
      if (a.Currency != b.Currency)
        throw new InvalidOperationException(
          $"Currencies doesn't match. a: {a.Currency}, b: {b.Currency}.");
    }
    
    private static Decimal Round(decimal value)
      => Math.Round(value, 4, MidpointRounding.AwayFromZero);

    public override string ToString()
      => $"{Amount} ({Currency})";
  }
}