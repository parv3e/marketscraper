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

                db.LoadAudits.Add(new LoadAudit() {Load = load, Message = "Initializing load" });
                db.SaveChanges();

                var json = new WebClient().DownloadString("https://esi.tech.ccp.is/latest/markets/10000002/orders/?page=1");

                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //serializer.MaxJsonLength = Int32.MaxValue;
                //var result = serializer.Deserialize<MarketOrder[]>(json);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketOrder[]>(json);
                load.MarketOrders.AddRange(result);

                db.Loads.Add(load);
                db.LoadAudits.Add(new LoadAudit() { Load = load, Message = "Completed load" });
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