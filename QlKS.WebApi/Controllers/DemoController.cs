using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace QlKS.WebApi.Controllers
{
    [RoutePrefix("api/demos")]
    public class DemoController : ApiController
    {
        [AllowAnonymous]
        [Route("getone")]
        [HttpGet]
        public IHttpActionResult get1()
        {
            return Ok("cHÀO BẠN CÔNG");
        }
        [Authorize]
        [Route("get2")]
        [HttpGet]
        public IHttpActionResult get2()
        {
            var Indetity = (ClaimsIdentity)User.Identity;
            return Ok(Indetity);
        }
        [Authorize(Roles ="admin")]
        [Route("get3")]
        [HttpGet]
        public IHttpActionResult get3()
        {
            var Indetity = (ClaimsIdentity)User.Identity;
            return Ok(Indetity);
        }

    }
}
