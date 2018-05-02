using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api
{
    [Table("MarketOrderHistory", Schema = "main")]
    public class MarketOrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [JsonProperty(PropertyName = "market_history_id")]
        public int MarketHistoryId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "type_id")]
        public int MarketTypeId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "date")]
        public DateTime DateFor { get; set; }

        [Required]
        [JsonProperty(PropertyName = "order_count")]
        public long OrderCount { get; set; }

        [Required]
        [JsonProperty(PropertyName = "volume")]
        public long Volume { get; set; }

        [Required]
        [JsonProperty(PropertyName = "highest")]
        public float Highest { get; set; }

        [Required]
        [JsonProperty(PropertyName = "average")]
        public float Average { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lowest")]
        public float Lowest { get; set; }
    }
}