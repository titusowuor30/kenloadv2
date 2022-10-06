using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class CalibrationCert
    {
        [Key]
        public int id { get; set; }
        public string station { get; set; }
        public string weighbridge { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public DateTime issued { get; set; }
        public DateTime expiry { get; set; }
        public string userid { get; set; }


    }
}
