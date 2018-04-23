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

                int currentOrderPage = 1;
                MarketOrder[] fetchedOrders = new MarketOrder[0];
                var cli = new WebClient();
                do
                {
                    var json = Loader.DownloadStringWithRetry(cli, $"https://esi.tech.ccp.is/latest/markets/10000002/orders/?page={currentOrderPage.ToString()}");
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

        public static void TearDown()
        {
            using (var db = new ApiContext())
            {
                var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketOrder]");
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [LoadAudit]");
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [Load]");
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketLocation]");
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketSystem]");
                objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketType]");
            }
        }

        public static void Setup()
        {
            using (var db = new ApiContext())
            {

                //CLEAR DOWN LOOKUPS
                var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
                //objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketLocation]");
                //objCtx.ExecuteStoreCommand("TRUNCATE TABLE [MarketSystem]");
                objCtx.ExecuteStoreCommand("DELETE FROM [MarketType]");
                int currentTypePage = 1;

                //create new web client
                var cli = new WebClient();

                //load systems
                var systemsJson = Loader.DownloadStringWithRetry(cli, "https://esi.tech.ccp.is/latest/universe/systems");
                var marketSystemIds = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(systemsJson);
                var marketSystems = UniverseNameResolver.Resolve<MarketSystem>(marketSystemIds);
                db.MarketSystems.AddRange(marketSystems);
                db.SaveChanges();

                //load market types
                //cannot guarantee here that ids will be either unique or distinct...
                List<int> allfetchedTypeIds = new List<int>();
                int[] fetchedTypeIds = new int[0];
                do
                {
                    System.Diagnostics.Debug.WriteLine($"Fetching market type page: {currentTypePage}");
                    var marketTypeJson = Loader.DownloadStringWithRetry(cli, $"https://esi.tech.ccp.is/latest/markets/10000002/types/?page={currentTypePage.ToString()}");
                    fetchedTypeIds = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(marketTypeJson);
                    allfetchedTypeIds.AddRange(fetchedTypeIds);
                    currentTypePage++;
                } while (fetchedTypeIds.Length > 0);

                var marketTypes = UniverseNameResolver.Resolve<MarketType>(allfetchedTypeIds.Distinct());
                db.MarketTypes.AddRange(marketTypes);
                db.SaveChanges();

            }
        }

        public static string UploadStringWithRetry(WebClient client, string url, string jsonIn)
        {
            for (var i = 0; i <= 10; i++)
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var ret = client.UploadString($"https://esi.tech.ccp.is/latest/universe/names/", jsonIn);
                    return ret;
                }
                catch (Exception)
                {
                    //don't care! can't hear, can't see.
                }
            }
            throw new ApplicationException("Unable to upload JSON within 10 tries.");
        }

        public static string DownloadStringWithRetry(WebClient client, string url)
        {
            for (var i = 0; i <= 10; i++)
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var ret = client.DownloadString(url);
                    return ret;
                }
                catch (Exception)
                {
                    //don't care! can't hear, can't see.
                }
            }
            throw new ApplicationException("Unable to download JSON within 10 tries.");
        }





    }
}