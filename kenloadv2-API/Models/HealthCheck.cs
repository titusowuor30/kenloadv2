using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class HealthCheck
    {
        [Key]
        public int id { get; set; }
        public string station { get; set; }
        public string ipaddress { get; set; }
        public string sysname { get; set; }
        public int status { get; set; }
        public DateTime lastchecked { get; set; }
        public int deletestatus { get; set; }
        public DateTime uptime { get; set; }
        public string message { get; set; }
        public string userid { get; set; }


    }
}
