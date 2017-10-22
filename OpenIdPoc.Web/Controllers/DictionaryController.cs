using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenIdPoc.Web.Models;

namespace OpenIdPoc.Web.Controllers
{
    public class DictionaryController : ApiController
    {
        // GET /api/dictionary
        public HttpResponseMessage Get()
        {
            var result = new List<DictionaryItem>
            {
                new DictionaryItem
                {
                    Term = "HTML",
                    Definition = "HyperText Markup Language"
                },
                new DictionaryItem
                {
                    Term = "CSS",
                    Definition = "Cascading Style Sheet"
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
