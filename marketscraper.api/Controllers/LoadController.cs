using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    public class LoadController : ApiController
    {

        [HttpPost]
        [Route("api/load/create")]
        public Load Create([FromBody]string value)
        {
            using (var db = new ApiContext())
            {
                //create new load
                var load = new Load()
                {
                    Created = DateTime.Now,
                    GUID = System.Guid.NewGuid().ToString(),
                    MarketOrders = new List<MarketOrder>()
                };

                for (var i = 0; i < 100; i++)
                {
                    load.MarketOrders.Add(new MarketOrder()
                    {
                        Duration = 1,
                        IsBuyOrder = true,
                        Issued = DateTime.Now,
                        LocationId = 0,
                        MinVolume = 0,
                        OrderId = 0,
                        Price = 0,
                        Range = "System",
                        SystemId = 0,
                        LoadId = 0,
                        TypeId = 0,
                        VolumeRemain = 0,
                        VolumeTotal = 0
                    });
                }

                db.Loads.Add(load);
                db.SaveChanges();

                return load;

            }
        }

        [HttpGet]
        [Route("api/load")]
        public IEnumerable<Load> GetList()
        {
            using (var db = new ApiContext())
            {
                return db.Loads.ToList();
            }
        }

        [HttpGet]
        [Route("api/load/{id}")]
        public Load GetSpecific(int id)
        {
            using (var db = new ApiContext())
            {
                return db.Loads.FirstOrDefault(l => l.LoadId == id);
            }
        }

    }
}