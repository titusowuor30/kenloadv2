using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class GVWoverloadCharges
    {
        [Key]
        public int id { get; set; }
        public int overloadkg { get; set; }
        public Double fee { get; set; }
        public string period { get; set; }

    }
}
