using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class AVWoverloadCharges
    {
        [Key]
        public int id { get; set; }
        public int? overloadkg { get; set; }
        public Double steeringaxle { get; set; }
        public Double singledriveaxle { get; set; }
        public Double tandemaxle { get; set; }
        public Double tridemaxle { get; set; }
        public string period { get; set; }

    }
}
