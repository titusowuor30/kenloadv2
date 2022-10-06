using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Transporters
    {
        [Key]
        public int id { get; set; }
        public string transporter_name { get; set; }
        public string transporter_address { get; set; }
        public string transporter_contact { get; set; }
        public string transporter_deletedstatus { get; set; }
        public string transporter_regdate { get; set; }
    }
}
