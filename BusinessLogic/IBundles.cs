using System.Collections.Generic;

namespace Bundler.BusinessLogic
{
    public interface IBundles
    {
        List<Bundle> GetBundlesHighesValueFirst();
    }
}
