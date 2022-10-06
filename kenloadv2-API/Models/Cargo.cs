using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Cargo
    {
        [Key]
        public int id { get; set; }
        public string cargo_name { get; set; }
        public string cargodetails { get; set; }  
        
    }
}
