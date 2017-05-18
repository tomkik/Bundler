using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public interface IRules
    {
        Bundle RecommendBundle(CustomerInfo info);
        string ValidateCustomBundle(CustomerInfo info, Bundle customBundle);
    }
}
