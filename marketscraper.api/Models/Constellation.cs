using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace marketscraper.api.Models
{
    public class Constellation
    {
        [JsonProperty(PropertyName = "constellation_id")]
        public int ConstellationId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "systems")]
        public int[] Systems { get; set; }

        [JsonProperty(PropertyName = "region_id")]
        public int RegionId { get; set; }
    }
}