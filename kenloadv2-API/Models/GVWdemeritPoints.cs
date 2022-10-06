using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class GVWdemeritPoints
    {
        [Key]
        public int id { get; set; }
        public int category { get; set; }
        public int point { get; set; }

    }
}
