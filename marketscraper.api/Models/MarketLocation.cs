using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api
{
    [Table("MarketLocation")]
    public class MarketLocation
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int LocationId { get; set; }

        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }
    }
}