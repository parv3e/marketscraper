using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace marketscraper.api
{
    public class LoadAudit
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "load_audit_id")]
        public int LoadAuditId { get; set; }

        [JsonIgnore]
        public virtual Load Load {get; set;}

        [Required]
        [ForeignKey("Load")]
        [JsonProperty(PropertyName = "load_id")]
        public int LoadId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}