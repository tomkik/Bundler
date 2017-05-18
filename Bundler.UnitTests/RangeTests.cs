using Bundler.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bundler.UnitTests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void WithinLowerBounds_ShouldReturnTrueWhenBigger()
        {
            Range sut = new Range{From = 5, To = 6 };
            Assert.IsTrue(sut.WithinLowerBounds(6));
        }

        [TestMethod]
        public void WithinLowerBounds_ShouldReturnTrueWhenEqual()
        {
            Range sut = new Range { From = 6, To = 6};
            Assert.IsTrue(sut.WithinLowerBounds(6));
        }

        [TestMethod]
        public void WithinLowerBounds_ShouldReturnFalseWhenSmaller()
        {
            Range sut = new Range { From = 6, To = 7};
            Assert.IsFalse(sut.WithinLowerBounds(5));
        }

        [TestMethod]
        public void WithinUpperBounds_ShouldReturnTrueWhenSmaller()
        {
            Range sut = new Range { From = 5, To = 6 };
            Assert.IsTrue(sut.WithinUpperBounds(5));
        }

        [TestMethod]
        public void WithinUpperBounds_ShouldReturnTrueWhenEqual()
        {
            Range sut = new Range { From = 0, To = int.MaxValue };
            Assert.IsTrue(sut.WithinUpperBounds(int.MaxValue));
        }
        
        [TestMethod]
        public void WithinLowerBounds_ShouldReturnFalseWhenBigger()
        {
            Range sut = new Range { From = 0, To = 6 };
            Assert.IsFalse(sut.WithinUpperBounds(int.MaxValue));
        }
    }
}
