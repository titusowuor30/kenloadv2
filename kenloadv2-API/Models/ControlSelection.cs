using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class ControlSelection
    {
        [Key]
        public int id { get; set; }
        public int roleid { get; set; }
        public int controlid { get; set; }
        public int select { get; set; }
   
    }
}
