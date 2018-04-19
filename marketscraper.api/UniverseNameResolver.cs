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
            for (var i = 0; i < ids.Count(); i += 500)
            {
                var idsToFetch = ids.Skip(i).Take(500);
                var idsJsonArray = Newtonsoft.Json.JsonConvert.SerializeObject(idsToFetch);
                var json = Loader.UploadStringWithRetry(client, "https://esi.tech.ccp.is/latest/universe/names/", idsJsonArray);
                result.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json));
            }

            return result;
        }

    }
}