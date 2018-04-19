using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api
{
    public class UniverseNameResolver
    {
        public static List<T> Resolve<T>(IEnumerable<int> ids)
        {
            var result = new List<T>();
            var client = new WebClient();
            for (var i = 0; i < ids.Count(); i += 100)
            {
                var idsToFetch = ids.Skip(i).Take(100);
                var idsJsonArray = Newtonsoft.Json.JsonConvert.SerializeObject(idsToFetch);
                var json = GetJsonWithRetry(client, idsJsonArray);
                result.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json));
                System.Threading.Thread.Sleep(250);
            }

            return result;
        }

        private static string GetJsonWithRetry(WebClient client, string jsonIn)
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
                    //don't care!                    
                }
            }
            throw new ApplicationException("Unable to fetch JSON within 10 tries.");
        }
    }
}