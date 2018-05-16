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
            Database.SetInitializer(new ApiContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new CodeFirstStoreFunctions.FunctionsConvention<ApiContext>("dbo"));
            modelBuilder.ComplexType<MarketStat>();
        }

        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadAudit> LoadAudits { get; set; }
        public DbSet<MarketOrder> MarketOrders { get; set; }
        public DbSet<MarketType> MarketTypes { get; set; }
        public DbSet<MarketOrderHistory> MarketOrderHistories { get; set; }

        [DbFunction("ApiConext", "GetStatistics")]
        public IQueryable<MarketStat> GetMarketStats(int loadId, float minRatio, float minOrderVolume)
        {

            //var loadIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("loadId", loadId);
            //var ratioParam = new System.Data.Entity.Core.Objects.ObjectParameter("minRatio", minRatio);
            //var minOrderVolumeParam = new System.Data.Entity.Core.Objects.ObjectParameter("minOrderVolume", minOrderVolume);

            try
            {
                var parameters = new List<System.Data.Entity.Core.Objects.ObjectParameter>();
                parameters.Add(new System.Data.Entity.Core.Objects.ObjectParameter("loadId", loadId));
                parameters.Add(new System.Data.Entity.Core.Objects.ObjectParameter("minRatio", minRatio));
                parameters.Add(new System.Data.Entity.Core.Objects.ObjectParameter("minOrderVolume", minOrderVolume));
                var ret = ((IObjectContextAdapter)this).ObjectContext.CreateQuery<MarketStat>("ApiContext.GetStatistics(@loadId, @minRatio, @minOrderVolume)", parameters.ToArray());
                return ret;
            }
            catch (Exception)
            {

                throw;
            }



        }
    }


}