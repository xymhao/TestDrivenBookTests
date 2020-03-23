using System;
using System.Collections;
using System.Linq.Expressions;
using DomainModel;

namespace TestDrivenBookTests
{
    public class Bank
    {
        private Hashtable rates = new Hashtable();

        public Money Reduce(MoneyExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public void AddRate(string from, string to, int rate)
        {
            rates.Add(new Pair(from, to), rate);
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to))
            {
                return 1;
            }
            var val = rates[new Pair(from, to)];
            if (val == null)
            {
                throw new ArgumentException("未获取到汇率。");
            }
            return (int)val;
        }
    }
}