using Bundler.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Bundler.UnitTests
{
    [TestClass]
    public class ProductsTests
    {
        [TestMethod]
        public void GetAccountRule_ShouldReturnAccountRequested()
        {
            Products sut = new Products();

            Products.AccountRule rule = sut.GetAccountRule(Products.Account.STUDENT_ACCOUNT);

            Assert.AreEqual(rule.Account, Products.Account.STUDENT_ACCOUNT);
        }

        [TestMethod]
        public void GetCardRule_ShouldReturnCardRequested()
        {
            Products sut = new Products();

            Products.CardRule rule = sut.GetCardRule(Products.Card.GOLD_CREDIT_CARD);

            Assert.AreEqual(rule.Card, Products.Card.GOLD_CREDIT_CARD);
        }
    }
}
