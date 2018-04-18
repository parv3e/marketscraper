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
        public static List<T> Resolve<T>(string idsJsonArray)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var json = client.UploadString($"https://esi.tech.ccp.is/latest/universe/names/", idsJsonArray);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}