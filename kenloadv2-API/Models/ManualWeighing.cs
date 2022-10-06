using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class ManualWeighing
    {
        
        [Key]
        public int id { get; set; }
        public DateTime date {get; set; }
        public String hour { get; set; }
        public int tickets { get; set; }
        public string userid { get; set; }
        public string station { get; set; }

        public int deletestatus { get; set; }


    }
}
