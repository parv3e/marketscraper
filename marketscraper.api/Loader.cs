using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api
{
    public class Loader
    {

        public static void LoadMarketOrders(int loadId)
        {

            using (var db = new ApiContext())
            {

                Load load = db.Loads.First(l => l.LoadId == loadId);

                int currentPage = 1;
                MarketOrder[] fetchedResults = new MarketOrder[0];

                do
                {
                    var json = new WebClient().DownloadString($"https://esi.tech.ccp.is/latest/markets/10000002/orders/?page={currentPage.ToString()}");
                    fetchedResults = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketOrder[]>(json);
                    load.MarketOrders.AddRange(fetchedResults);
                    db.LoadAudits.Add(new LoadAudit() { Load = load, Message = $"Fetched page {currentPage} of Market Orders." });
                    db.SaveChanges();
                    currentPage++;
                } while (fetchedResults.Length > 0);

                load.Completed = DateTime.Now;
                db.LoadAudits.Add(new LoadAudit() { Load = load, Message = "Completed load." });
                db.SaveChanges();
            }


        }

    }
}