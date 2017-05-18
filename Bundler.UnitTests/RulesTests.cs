using Bundler.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bundler.UnitTests
{
    [TestClass]
    public class RulesTests
    {
        Mock<IProducts> FakeProducts;
        Mock<IBundles> FakeBundles;

        [TestInitialize]
        public void Init()
        {
            FakeProducts = new Mock<IProducts>(MockBehavior.Loose);
            FakeBundles = new Mock<IBundles>(MockBehavior.Loose);
        }

        [TestMethod]
        public void RecommendBundle_ShouldRecomendGoldEvenMoreLessValueAreAvailable()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(false);

            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(true);

            FakeBundles.Setup(x => x.GetBundlesHighesValueFirst()).Returns(new List<Bundle>() {
                new Bundle { BundleName = Bundle.Name.GOLD, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoPositive.Object } },
                new Bundle { BundleName = Bundle.Name.STUDENT, Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object } },
                new Bundle { BundleName = Bundle.Name.JUNIOR_SAVER, Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object } },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            Assert.IsTrue(sut.RecommendBundle(new CustomerInfo()).BundleName == Bundle.Name.GOLD);
        }

        [TestMethod]
        public void RecommendBundle_ShouldRecommendJunior()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(false);

            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(true);

            FakeBundles.Setup(x => x.GetBundlesHighesValueFirst()).Returns(new List<Bundle>() {
                new Bundle { BundleName = Bundle.Name.GOLD, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object } },
                new Bundle { BundleName = Bundle.Name.STUDENT, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object } },
                new Bundle { BundleName = Bundle.Name.JUNIOR_SAVER, Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object } },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            Assert.IsTrue(sut.RecommendBundle(new CustomerInfo()).BundleName == Bundle.Name.JUNIOR_SAVER);
        }

        [TestMethod]
        public void RecommendBundle_ShouldReturnNull()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(false);

            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.SatisfiesCondition(It.IsAny<CustomerInfo>())).Returns(true);

            FakeBundles.Setup(x => x.GetBundlesHighesValueFirst()).Returns(new List<Bundle>() {
                new Bundle { BundleName = Bundle.Name.GOLD, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object } },
                new Bundle { BundleName = Bundle.Name.STUDENT, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object } },
                new Bundle { BundleName = Bundle.Name.JUNIOR_SAVER, Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object } },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            Assert.IsNull(sut.RecommendBundle(new CustomerInfo()));
        }

        [TestMethod]
        public void GetLeastErrorsString_ShouldReturnNull()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            List<string>[] errors = new List<string>[]
            {
                new List<string>() { "one", "two"},
                new List<string>() { },
                new List<string>() { "one", "two", "three"},
            };

            Assert.IsNull(sut.GetLeastErrorsString(errors));
        }

        [TestMethod]
        public void GetLeastErrorsString_ShouldReturnStringWithOneError()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            List<string>[] errors = new List<string>[]
            {
                new List<string>() { "one", "two"},
                new List<string>() { "oneError"},
                new List<string>() { "one", "two", "three"},
            };

            Assert.IsTrue(sut.GetLeastErrorsString(errors).Contains("oneError"));
        }

        [TestMethod]
        public void GetLeastErrorsString_ShouldReturnStringWithThreeErrors()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            List<string>[] errors = new List<string>[]
            {
                new List<string>() { "dog", "fog", "frog"},
                new List<string>() { "make", "cake", "lake", "fake"},
                new List<string>() { "one", "two", "three", "four", "five"},
            };

            Assert.IsTrue(sut.GetLeastErrorsString(errors).Contains("dog") &&
                sut.GetLeastErrorsString(errors).Contains("fog") &&
                sut.GetLeastErrorsString(errors).Contains("frog"));
        }

        [TestMethod]
        public void ValidateCards_ShouldReturnThreeErrors()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>(){ "smth"});

            FakeProducts.Setup(x => x.GetCardRule(It.IsAny<Products.Card>())).Returns(new Products.CardRule {
                 Card = Products.Card.CREDIT_CARD,
                 Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCards(new CustomerInfo(), new List<Products.Card>() { new Products.Card(), new Products.Card(), new Products.Card() });
            int occurances = Regex.Matches(errors, "smth").Count;

            Assert.IsTrue(occurances == 3);
        }

        [TestMethod]
        public void ValidateCards_ShouldReturnNoErrors()
        {
            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { });

            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { "smth" });

            FakeProducts.Setup(x => x.GetCardRule(It.IsAny<Products.Card>())).Returns(new Products.CardRule
            {
                Card = Products.Card.CREDIT_CARD,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCards(new CustomerInfo(), new List<Products.Card>() { new Products.Card(), new Products.Card(), new Products.Card() });

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateCards_ShouldReturnNoErrorsWhenNoCards()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCards(new CustomerInfo(), null);

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateAccount_ShouldReturnThreeErrors()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { "smth", "smth", "smth" });

            FakeProducts.Setup(x => x.GetAccountRule(It.IsAny<Products.Account>())).Returns(new Products.AccountRule
            {
                Account = Products.Account.PENSIONER_ACCOUNT,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateAccount(new CustomerInfo(), Products.Account.PENSIONER_ACCOUNT);
            int occurances = Regex.Matches(errors, "smth").Count;

            Assert.IsTrue(occurances == 3);
        }

        [TestMethod]
        public void ValidateAccount_ShouldReturnNoErrors()
        {
            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { });

            FakeProducts.Setup(x => x.GetAccountRule(It.IsAny<Products.Account>())).Returns(new Products.AccountRule
            {
                Account = Products.Account.PENSIONER_ACCOUNT,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateAccount(new CustomerInfo(), Products.Account.PENSIONER_ACCOUNT);

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateAccount_ShouldReturnNoErrorsWhenNoAccount()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateAccount(new CustomerInfo(), Products.Account.NONE);

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateCardsAccountMatch_ShouldReturnNullIfValidCombination()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCardsAccountMatch(new List<Products.Card>() { Products.Card.DEBIT_CARD }, Products.Account.CURRENT_ACCOUNT_PLUS);

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateCardsAccountMatch_ShouldReturnErrorIfWrongAccount()
        {
            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCardsAccountMatch(new List<Products.Card>() { Products.Card.DEBIT_CARD }, Products.Account.JUNIOR_SAVER_ACCOUNT);

            Assert.IsFalse(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateCustomBundle_ShouldReturnNoErrorIfAllOk()
        {
            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { });

            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { "smth" });

            FakeProducts.Setup(x => x.GetCardRule(It.IsAny<Products.Card>())).Returns(new Products.CardRule
            {
                Card = Products.Card.CREDIT_CARD,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object, FakeCustomerInfoNegative.Object },
            });

            FakeProducts.Setup(x => x.GetAccountRule(It.IsAny<Products.Account>())).Returns(new Products.AccountRule
            {
                Account = Products.Account.PENSIONER_ACCOUNT,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCustomBundle(new CustomerInfo(), new Bundle {
                Account = Products.Account.STUDENT_ACCOUNT,
                Cards = new List<Products.Card>() { Products.Card.DEBIT_CARD},
            });

            Assert.IsTrue(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public void ValidateCustomBundle_ShouldReturnErrorIfCardNok()
        {
            Mock<ICustomerInfo> FakeCustomerInfoPositive = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoPositive.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { });

            Mock<ICustomerInfo> FakeCustomerInfoNegative = new Mock<ICustomerInfo>(MockBehavior.Loose);
            FakeCustomerInfoNegative.Setup(x => x.GetValidationErrors(It.IsAny<CustomerInfo>())).Returns(new List<string>() { "smth" });

            FakeProducts.Setup(x => x.GetCardRule(It.IsAny<Products.Card>())).Returns(new Products.CardRule
            {
                Card = Products.Card.CREDIT_CARD,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoNegative.Object, FakeCustomerInfoNegative.Object },
            });

            FakeProducts.Setup(x => x.GetAccountRule(It.IsAny<Products.Account>())).Returns(new Products.AccountRule
            {
                Account = Products.Account.PENSIONER_ACCOUNT,
                Rules = new List<ICustomerInfo>() { FakeCustomerInfoPositive.Object, FakeCustomerInfoNegative.Object },
            });

            Rules sut = new Rules(FakeBundles.Object, FakeProducts.Object);

            string errors = sut.ValidateCustomBundle(new CustomerInfo(), new Bundle
            {
                Account = Products.Account.STUDENT_ACCOUNT,
                Cards = new List<Products.Card>() { Products.Card.DEBIT_CARD },
            });

            Assert.IsFalse(string.IsNullOrEmpty(errors));
        }
    }
}
