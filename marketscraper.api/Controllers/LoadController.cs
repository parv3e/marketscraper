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

        //Initializes new load and spawns new Loader thread which does the loading.
        //Returns without waiting for the load to finish
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

                db.Loads.Add(load);
                db.LoadAudits.Add(new LoadAudit() {Load = load, Message = "Initializing load" });
                db.SaveChanges();

                System.Threading.Thread loaderThread = new System.Threading.Thread(() => Loader.LoadMarketOrders(load.LoadId));
                loaderThread.Start();

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