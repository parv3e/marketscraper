using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api.Models
{
    public class SpaceSystem

    {
        [JsonProperty(PropertyName = "system_id")]
        public int SystemId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "security_status")]
        public float SecurityStatus { get; set; }

        [JsonProperty(PropertyName = "security_class")]
        public string SecurityClass { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "stations")]
        public int[] Stations { get; set; }
    }
}