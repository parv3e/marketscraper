using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    public class MarketOrderController : ApiController
    {
        [HttpGet]
        [Route("api/marketorder/{id}")]
        public MarketOrder GetSpecific(int id)
        {
            using (var db = new ApiContext())
            {
                return db.MarketOrders.FirstOrDefault(mo => mo.OrderId == id);
            }
        }
        
        [HttpGet]
        [Route("api/marketorder")]
        public IEnumerable<MarketOrder> GetList([FromUri]  int loadId)
        {
            using (var db = new ApiContext())
            {
                return db.MarketOrders.Where(mo => mo.LoadId == loadId).ToArray();
            }
        }


    }
}