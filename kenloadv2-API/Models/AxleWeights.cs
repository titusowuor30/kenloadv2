using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AxleWeights
    {
        [Key]
        public int id { get; set; }
        public string wbrg_ticket_no { get; set; }
        public double axle_actualweight { get; set; }
        public double axle_permissibleweight { get; set; }
        public double axle_variantweight { get; set; }
        public DateTime axle_time { get; set; }
        public string axle_group { get; set; }
        public string axle_grouping { get; set; }
        public DateTime timestamp { get; set; }
        public int axle_number { get; set; }
        public double fee { get; set; }
        public int weighbridgetransactionsid { get; set; }
    }
}
