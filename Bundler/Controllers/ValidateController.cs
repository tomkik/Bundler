using System.Net.Http;
using System.Web.Http;
using Bundler.BusinessLogic;
using Bundler.Code;

namespace Bundler.Controllers
{
    public class ValidateController : ApiController
    {
        private Rules Rules = new Rules(new Bundles(), new Products());

        /// <summary>
        /// Validates customer provided products against personal information and returns validation errors
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(UserInfo info)
        {
            string errors = Rules.ValidateCustomBundle(info.ConvertToCustomerInfo(), info.ConvertToBundle());

            var response = Request.CreateResponse<string>(
                string.IsNullOrEmpty(errors) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest, errors);

            return response;
        }
    }
}
