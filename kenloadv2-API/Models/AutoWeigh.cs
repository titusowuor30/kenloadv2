using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class AutoWeigh
    {
        [Key]
        public int id { get; set; }
        public string wbt_no { get; set; }
        public string nplate { get; set; }
        public double deck1 { get; set; }
        public double deck2 { get; set; }
        public double deck3 { get; set; }
        public double deck4 { get; set; }
        public double gvw { get; set; }
        public DateTime? autodatetime { get; set; }
        public DateTime? weighdate { get; set; }
        public string  autoweighbridge { get; set; }
        public string autouser { get; set; }
        public string ipaddress { get; set; }
        public string autostatus { get; set; }
        public string anpr { get; set; }
        public string wbtno { get; set; }
        public string anprb { get; set; }
    }
}
