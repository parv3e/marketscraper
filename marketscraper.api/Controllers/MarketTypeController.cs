using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    public class MarketTypeController : ApiController
    {
        [HttpGet]
        [Route("api/markettypes")]
        public MarketType[] GetAll()
        {
            using (var db = new ApiContext())
            {
                return db.MarketTypes.ToArray();
            }
        }
    }
}
