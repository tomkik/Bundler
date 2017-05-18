using System.Net.Http;
using System.Web.Http;
using Bundler.BusinessLogic;
using Bundler.Repository;
using Bundler.Code;

namespace Bundler.Controllers
{
    public class RecommendController : ApiController
    {
        
        private Rules Rules = new Rules(new Bundles(), new Products());
        private Logger Logger = new Logger();
        /*
        public BundlerController(IRules rules)
        {
            Rules = rules;
        }
        */

        /// <summary>
        /// Accepts user provided answers and returns account/card bundle accordingly
        /// </summary>
        /// <param name="info">User answers</param>
        /// <returns>Bundle suggested</returns>
        public HttpResponseMessage Post(UserInfo info)
        {
            CustomerInfo customerInfo = info.ConvertToCustomerInfo();

            Logger.LogInfo(customerInfo);
            Bundle bundle = Rules.RecommendBundle(customerInfo);
            
            var response = Request.CreateResponse<Bundle>(System.Net.HttpStatusCode.Created, bundle);

            return response;
        }
    }
}
