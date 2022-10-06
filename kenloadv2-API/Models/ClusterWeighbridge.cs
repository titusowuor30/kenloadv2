using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class ClusterWeighbridge
    {
        [Key]
        public int id { get; set; }
        public string cluster_code { get; set; }
        public string cluster_name { get; set; }
        public string prefix { get; set; }

    }
}
