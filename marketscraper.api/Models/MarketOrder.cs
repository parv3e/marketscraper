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
    [Table("MarketOrder", Schema = "main")]
    public class MarketOrder
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "order_id")]
        public long OrderId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "type_id")]
        public int TypeId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "location_id")]
        public long LocationId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "system_id")]
        public int SystemId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "volume_total")]
        public int VolumeTotal { get; set; }

        [Required]
        [JsonProperty(PropertyName = "volume_remain")]
        public int VolumeRemain { get; set; }

        [Required]
        [JsonProperty(PropertyName = "min_volume")]
        public int MinVolume { get; set; }

        [Required]
        [JsonProperty(PropertyName = "price")]
        public float Price { get; set; }

        [Required]
        [JsonProperty(PropertyName = "is_buy_order")]
        public bool IsBuyOrder { get; set; }

        [Required]
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        [Required]
        [JsonProperty(PropertyName = "issued")]
        public DateTime Issued { get; set; }

        [Required]
        [JsonProperty(PropertyName = "range")]
        public string Range { get; set; }

        [JsonIgnore]
        public virtual Load Load { get; set; }

        [Required]
        [ForeignKey("Load")]
        [JsonProperty(PropertyName = "load_id")]
        public int LoadId { get; set; }
    }
}
