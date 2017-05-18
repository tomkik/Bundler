using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public interface ICustomerInfo
    {
        List<string> GetValidationErrors(CustomerInfo condition);
        bool SatisfiesCondition(CustomerInfo condition);
    }
}
