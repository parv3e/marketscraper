using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api
{
    [Table("Load")]
    public class Load
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "load_id")]
        public int LoadId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "guid")]
        public string GUID { get; set; }
        [Required]

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public DateTime? Completed { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "has_completed")]
        public bool HasCompleted {
            get {
                return this.Completed.HasValue;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual List<MarketOrder> MarketOrders { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual List<MarketType> MarketTypes { get; set; }
    }
}
