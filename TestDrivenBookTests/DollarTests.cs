using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using DomainModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDrivenBookTests
{
    [TestClass]
    public class DollarTests
    {
        private string CHF = "CHF";
        private string USD = "USD";
        //todo 1.当瑞士法郎与美元的兑换率2：1的时候，5美元+10法郎=10美元
        //todo 2.5美元*2=10美元 done
        //todo 3.Dollar 类副作用
        //todo 4.Amount应为私有
        //todo 5.实现equal

        [TestMethod]
        public void TestEquality()
        {
            var dollar = Money.Dollar(5);
            var franc = Money.Franc(5);
            Assert.IsFalse(dollar.Equals(franc));
            Assert.IsFalse(franc.Equals(dollar));
        }

        [TestMethod]
        public void Should_Times()
        {
            var money = Money.Dollar(5);
            Assert.IsTrue(Money.Dollar(10).Equals(money.Times(2)));
            Assert.IsTrue(Money.Dollar(15).Equals(money.Times(3)));
        }

        [TestMethod]
        public void Should_Current()
        {
            Assert.AreEqual(USD, Money.Dollar(1).GetCurrency());
            Assert.AreEqual(CHF, Money.Franc(1).GetCurrency());
        }

        [TestMethod]
        public void Should_Different_Class_Equal()
        {
            Assert.IsTrue(new Money(10, CHF).Equals(Money.Franc(10)));
        }


        [TestMethod]
        public void Should_Reduce_Sum()
        {
            var sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Bank bank = new Bank();
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(7), result);
        }

        [TestMethod]
        public void TestPlusReturnSum()
        {
            Money five = Money.Dollar(5);
            Expression result = five.Plus(five);
            Sum sum = (Sum)result;
            Assert.AreEqual(five, sum.augend);
            Assert.AreEqual(five, sum.addend);
        }

        [TestMethod]
        public void TestReduceSum()
        {
            var sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Bank bank = new Bank();
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(7), result);
        }

        [TestMethod]
        public void Test_Money_Reduce()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.Dollar(1), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [TestMethod]
        public void Should_ReduceMoney_DifferentCurrency()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Assert.AreEqual(2, actual: bank.Rate(CHF, USD));
        }

        [TestMethod]
        public void Should_Rate_1_When_Same()
        {
            Bank bank = new Bank();
            Assert.AreEqual(1, actual: bank.Rate(USD, USD));
        }

        [TestMethod]
        public void Should_Plus()
        {
            var sum = Money.Dollar(5).Plus(Money.Dollar(5));
            Assert.AreEqual(Money.Dollar(10), sum.Reduce(new Bank(), USD));
        }

        [TestMethod]
        public void Should_Different_Plus()
        {
            MoneyExpression sum = Money.Dollar(5).Plus(Money.Franc(10));
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Assert.AreEqual(Money.Dollar(10), sum.Reduce(bank, USD));
        }

        [TestMethod]
        public void Should_Test_Sum_Money()
        {
            MoneyExpression fiveBucks = Money.Dollar(5);
            MoneyExpression tenFrancs = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            MoneyExpression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
            Money result = bank.Reduce(sum, USD);
            Assert.AreEqual(Money.Dollar(15), result);
        }

        [TestMethod]
        public void Test_Sum_Times()
        {
            MoneyExpression fiveBucks = Money.Dollar(5);
            MoneyExpression tenFrancs = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            MoneyExpression sum = new Sum(fiveBucks, tenFrancs).Times(2);
            Money result = bank.Reduce(sum, USD);
            Assert.AreEqual(Money.Dollar(20), result);
        }

    }
}
