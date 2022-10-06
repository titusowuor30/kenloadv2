using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AxleWeightConfig
    { 
        [Key]
        public int id { get; set; }
        [StringLengthAttribute(20)]
        public string axle_code{ get; set; }
        [StringLengthAttribute(20)]
        public string axle_name { get; set; }
        public int axle_number { get; set; }
        public string axle_camea { get; set; }
    
    }
}
