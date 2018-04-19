using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace marketscraper.api
{
    public class ApiContext : DbContext
    {
        public ApiContext() : base("name=marketscraperConnectionString")
        {

        }
        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadAudit> LoadAudits { get; set; }
        public DbSet<MarketOrder> MarketOrders { get; set; }
        public DbSet<MarketType> MarketTypes { get; set; }
        public DbSet<MarketSystem> MarketSystems { get; set; }
        public DbSet<MarketLocation> MarketLocations { get; set; }
    }


}