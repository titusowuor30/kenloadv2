using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class VehicleMake
    {
        [Key]
        public int id { get; set; }
        public string vehiclemake { get; set; }
        public string vehiclemake_desc { get; set; }
        public DateTime vehiclemake_regdate { get; set; }

    }
}
