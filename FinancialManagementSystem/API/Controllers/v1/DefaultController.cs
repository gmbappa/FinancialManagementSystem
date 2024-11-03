using System.Web.Http;

namespace API.Controllers.v1
{
    [RoutePrefix("api/v{apiVersion}/default")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route("Index")]
        public IHttpActionResult Index()
        {          
            return Ok("Account data get successfully.");
        }

        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save(string id)
        {
            return Ok("Account Save successfully.");
        }
    }
}
