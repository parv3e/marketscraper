using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace marketscraper.api.Controllers
{
    public class LoadAuditController : ApiController
    {
        [Route("api/loadaudit")]
        public IEnumerable<LoadAudit> Get([FromUri] int loadId) 
        {
            using (var db = new ApiContext()) {
                return db.LoadAudits.Where(la => la.LoadId == loadId).ToArray();
            }
        }
    }
}
