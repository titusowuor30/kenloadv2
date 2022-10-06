using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Origin
    {

        [Key]
        public int id { get; set; }
        public string origin_name { get; set; }
        public bool deletedstatus { get; set; }
    }
}
