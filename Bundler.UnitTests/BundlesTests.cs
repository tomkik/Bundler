using Bundler.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Bundler.UnitTests
{
    [TestClass]
    public class BundlesTests
    {
        [TestMethod]
        public void GetBundlesHighesValueFirst_ShouldReturnBundlesInDescendingOrder()
        {
            Bundles sut = new Bundles();

            List<Bundle> bundles = sut.GetBundlesHighesValueFirst();
            List<Bundle> reorderedBundles = bundles.OrderByDescending(b => b.Value).ToList();

            CollectionAssert.AreEqual(reorderedBundles, bundles);
        }            
    }
}
