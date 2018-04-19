using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace marketscraper.api
{
    [Table("MarketType")]
    public class MarketType
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int MarketTypeId {get; set;}

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


    }
}