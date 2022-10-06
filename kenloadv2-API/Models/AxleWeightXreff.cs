using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AxleWeightXreff
    {
        [Key]
        public int id { get; set; }
        [StringLengthAttribute(20)]
        public string axle_code { get; set; }
        public int axle_code2 { get; set; }
        public double axle_legalweight { get; set; }
        public string axle_group { get; set; }
        public string axle_grouping { get; set; }
        public string axle_deckgrouping { get; set; }
        public string axle_typeoftyres { get; set; }
        public string cameawim { get; set; }

    }
}
