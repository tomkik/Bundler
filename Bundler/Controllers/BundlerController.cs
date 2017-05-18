using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bundler.Controllers
{
    public class BundlerController : ApiController
    {
        public string[] Get()
        {
            return new string[]
            {
             "Hello",
             "World"
            };
        }

        public class Contact
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public HttpResponseMessage Post(Contact contact)
        {
            var response = Request.CreateResponse<Contact>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }
    }
}
