using Bundler.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Bundler.UnitTests
{
    [TestClass]
    public class CustomerInfoTests
    {
        [TestMethod]
        public void GetValidationErrors_ShouldReturnAgeTooYoung()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(false);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors[0], CustomerInfo.AgeMustBeGreaterThan);
        }

        [TestMethod]
        public void GetValidationErrors_ShouldReturnAgeTooOld()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(false);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors[0], CustomerInfo.AgeMustBeLessThan);
        }

        [TestMethod]
        public void GetValidationErrors_ShouldReturnIncomeTooSmall()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(false);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors[0], CustomerInfo.IncomeMustBeGreaterThan);
        }

        [TestMethod]
        public void GetValidationErrors_ShouldReturnIncomeTooBig()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(false);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors[0], CustomerInfo.IncomeMustBeLessThan);
        }

        [TestMethod]
        public void GetValidationErrors_ShouldReturnPersonMustBeDifferent()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.STUDENT,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors[0], CustomerInfo.PersonMustBe + sut.Status);
        }

        [TestMethod]
        public void GetValidationErrors_ShouldReturnAgeTooSmallAgeTooBigIncomeTooSmallIncomeTooBigPersonMustBeDifferent()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(false);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(false);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(false);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(false);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.STUDENT,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            List<string> errors = sut.GetValidationErrors(test);

            Assert.AreEqual(errors.Count, 5);
            Assert.IsTrue(errors.Contains(CustomerInfo.AgeMustBeGreaterThan));
            Assert.IsTrue(errors.Contains(CustomerInfo.AgeMustBeLessThan));
            Assert.IsTrue(errors.Contains(CustomerInfo.IncomeMustBeGreaterThan));
            Assert.IsTrue(errors.Contains(CustomerInfo.IncomeMustBeLessThan));
            Assert.IsTrue(errors.Contains(CustomerInfo.PersonMustBe + sut.Status));
        }

        [TestMethod]
        public void SatisfiesCondition_ShouldReturnFalseWhenOneErrorExist()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.STUDENT,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            Assert.IsFalse(sut.SatisfiesCondition(test));
        }

        [TestMethod]
        public void SatisfiesCondition_ShouldReturnTrueWhenNoErrors()
        {
            Mock<IRange> FakeAgeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeAgeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeAgeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            Mock<IRange> FakeIncomeRange = new Mock<IRange>(MockBehavior.Strict);
            FakeIncomeRange.Setup(x => x.WithinLowerBounds(It.IsAny<int>())).Returns(true);
            FakeIncomeRange.Setup(x => x.WithinUpperBounds(It.IsAny<int>())).Returns(true);

            CustomerInfo sut = new CustomerInfo
            {
                Age = FakeAgeRange.Object,
                Income = FakeIncomeRange.Object,
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            CustomerInfo test = new CustomerInfo
            {
                Age = new BusinessLogic.Range { From = 18, To = int.MaxValue },
                Income = new BusinessLogic.Range { From = 40001, To = int.MaxValue },
                Status = CustomerInfo.PersonStatus.OTHER,
            };

            Assert.IsTrue(sut.SatisfiesCondition(test));
        }
    }
}
