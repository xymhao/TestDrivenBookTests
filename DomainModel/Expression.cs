using System;
using System.Collections.Generic;
using System.Text;
using TestDrivenBookTests;

namespace DomainModel
{
    public abstract class MoneyExpression : System.Linq.Expressions.Expression
    {
        public abstract Money Reduce(Bank bank, string to);

        public abstract MoneyExpression Times(int multiplier);
    }
}
