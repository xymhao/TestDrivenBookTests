using System.Linq.Expressions;
using DomainModel;

namespace TestDrivenBookTests
{
    public class Sum : MoneyExpression
    {
        public MoneyExpression augend;
        public MoneyExpression addend;

        public Sum(MoneyExpression augend, MoneyExpression addend)
        {
            this.augend = augend;
            this.addend = addend;
        }

        public override Money Reduce(Bank bank, string to)
        {
            var auAmount = augend.Reduce(bank, to);
            var adAmount = addend.Reduce(bank, to);
            return new Money(auAmount.Amount + adAmount.Amount, to);
        }

        public MoneyExpression Plus(MoneyExpression fiveBucks)
        {
            return new Sum(this, addend);
        }

        public override MoneyExpression Times(int multiplier)
        {
            return new Sum(augend.Times(multiplier), addend.Times(multiplier));
        }
    }
}