using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api
{
    public class MarketStat
    {
        [JsonProperty(PropertyName = "market_type_id")]
        public int MarketTypeId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ratio")]
        public double Ratio { get; set; }

        public double AvgSellMarketSize { get; set; }

        public double AvgBuyMarketSize { get; set; }

        public double MaxBuyPrice { get; set; }

        public double MinSellPrice { get; set; }

        public double BuyRemainVolume { get; set; }

        public double BuyTotalVolume { get; set; }

        public double SellRemainVolume { get; set; }

        public double SellTotalVolume { get; set; }

        public System.Int64 AvgOrderCount { get; set; }

        public System.Int64 AvgOrderVolume { get; set; }
    }
}