using System;
using System.Linq.Expressions;
using DomainModel;

namespace TestDrivenBookTests
{
    public class Money : MoneyExpression
    {
        private Bank _bank;
        public int Amount { get; }

        private string Currency { get; }

        public Money(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override MoneyExpression Times(int multiplier)
        {
            return new Money(multiplier * Amount, Currency);
        }

        public string GetCurrency()
        {
            return Currency;
        }

        public override bool Equals(object dollar)
        {
            if (dollar == null)
            {
                return false;
            }
            var val = (Money)dollar;
            return Amount.Equals(val.Amount)
                && val.Currency.Equals(Currency);
        }

        public override int GetHashCode()
        {
            return Amount;
        }

        public static bool operator ==(Money val, Money val2)
        {
            if (val is null || val2 is null)
            {
                return false;
            }
            return val.Equals(val2);
        }

        public static bool operator !=(Money val, Money val2)
        {
            return !(val == val2);
        }

        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }

        public override Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(Currency, to);
            return new Money(Amount / rate, to);
        }


        public MoneyExpression Plus(Money money)
        {
            return new Sum(this, money);
        }
    }
}