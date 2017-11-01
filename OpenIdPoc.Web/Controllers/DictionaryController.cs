using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenIdPoc.Web.Models;

namespace OpenIdPoc.Web.Controllers
{
    [Authorize]
    public class DictionaryController : ApiController
    {
        // App key from ad portal : 7fdeedVwCL9KyGk2F2v3dVAY+QwyTI2akK6tBzcQ3R4=


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
            
            var model = new DictionaryResponseModel
            {
                DictionaryItems = result,
                UserData = new UserData
                {
                    UserName = User.Identity.Name

                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
