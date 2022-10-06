using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class RoadAuthority
    {
        [Key]
        public int id { get; set; }
        public string authority_code { get; set; }
        public string authority_name { get; set; }
        public string prefix { get; set; }
        public bool deletedstatus { get; set; }
    }
}
