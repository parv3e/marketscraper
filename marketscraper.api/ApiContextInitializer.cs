using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace marketscraper.api
{
    public class ApiContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApiContext>
    {
        public override void InitializeDatabase(ApiContext context)
        {
            base.InitializeDatabase(context);
            context.Database.ExecuteSqlCommand(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/DropGetStatistics.sql")));
            context.Database.ExecuteSqlCommand(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/GetStatistics.sql")));
        }
    }
}