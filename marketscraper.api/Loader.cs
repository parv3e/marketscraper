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


                int currentTypePage = 1;
                MarketType[] fetchedTypes = new MarketType[0];
                do
                {
                    var json = new WebClient().DownloadString($"https://esi.tech.ccp.is/latest/markets/10000002/types/?page={currentTypePage.ToString()}");
                    fetchedTypes = UniverseNameResolver.Resolve<MarketType>(json).ToArray();
                    load.MarketTypes.AddRange(fetchedTypes);
                    db.LoadAudits.Add(new LoadAudit() { Load = load, Message = $"Fetched page {currentTypePage} of Market Types." });
                    db.SaveChanges();
                    currentTypePage++;
                } while (fetchedTypes.Length > 0);

                int currentOrderPage = 1;
                MarketOrder[] fetchedOrders = new MarketOrder[0];
                do
                {
                    var json = new WebClient().DownloadString($"https://esi.tech.ccp.is/latest/markets/10000002/orders/?page={currentOrderPage.ToString()}");
                    fetchedOrders = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketOrder[]>(json);
                    load.MarketOrders.AddRange(fetchedOrders);
                    db.LoadAudits.Add(new LoadAudit() { Load = load, Message = $"Fetched page {currentOrderPage} of Market Orders." });
                    db.SaveChanges();
                    currentOrderPage++;
                } while (fetchedOrders.Length > 0);


                load.Completed = DateTime.Now;
                db.LoadAudits.Add(new LoadAudit() { Load = load, Message = "Completed load." });
                db.SaveChanges();
            }


        }

        

    }
}