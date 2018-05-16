using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    public class MarketStatController : ApiController
    {
        [HttpGet]
        [Route("api/marketstats")]
        public MarketStat[] GetSpecific([FromUri] int loadId, [FromUri] float minRatio, [FromUri] float minOrderVolume)
        {
            using (var db = new ApiContext())
            {
                var ret = db.GetMarketStats(loadId, minRatio, minOrderVolume);
                return ret.ToArray();
            }
        }
    }
}
