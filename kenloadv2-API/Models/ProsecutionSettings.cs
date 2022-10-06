using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class ProsecutionSettings
    {
        [Key]
        public int id { get; set; }
        public string alongroad { get; set; }
        public string district { get; set; }
        public string weighbridge { get; set; }
        public string county { get; set; }
        public string court { get; set; }
        public string complainant { get; set; }
        public string police { get; set; }
        public string inspector { get; set; }
        public string movevehicle { get; set; }
    }
}
