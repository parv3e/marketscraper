using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    
    public class ConfigurationController : ApiController
    {
        [HttpPost]
        [Route("api/configuration/setup")]
        public void Setup()
        {
            Loader.Setup();
        }
    }
}
