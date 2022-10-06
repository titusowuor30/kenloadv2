using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class Census
    {
        [Key]
        public int id { get; set; }
        public string censusid { get; set; }
        public DateTime date { get; set; }
        public string shift { get; set; }
        public String route { get; set; }
        public String hour { get; set; }
        public int truck7t { get; set; }
        public int truck357t { get; set; }
        public int buses { get; set; }
        public string station { get; set; }
        public string userid { get; set; }
        public int deletestatus { get; set; }
    }
}
