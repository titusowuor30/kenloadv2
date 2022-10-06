using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class Events
    {
        [Key]
        public int id { get; set; }
        public string events { get; set; }
        public string userid { get; set; }
        public DateTime datetime { get; set; }
        public int confirmed { get; set; }
        public string confirmedby { get; set; }
        public string desc { get; set; }

    }
}
