using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class YardList
    {
        [Key]
        public int id { get; set; }
        public string vehreg { get; set; }
        public string status { get; set; }
        public string userid { get; set; }
        public DateTime datetime { get; set; }
        public string wbtno { get; set; }
        public string specialrelease { get; set; }
        public string spreleaseno { get; set; }
        public string station { get; set; }
        public string caseid { get; set; }
        public DateTime yardtimeout { get; set; }
        public int inspected { get; set; }
        public int WeighbridgeTransactionsid { get; set; }

    }
}
