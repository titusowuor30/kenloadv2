using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class PermitURL
    {
        [Key]
        public int id { get; set; }
        public string permiturl { get; set; }
        
    }
}
