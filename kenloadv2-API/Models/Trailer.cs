using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Trailer
    {
        [Key]
        public int id { get; set; }
        public string trailer_code { get; set; }
        public string trailer_name { get; set; }
        public bool trailer_deletedstatus { get; set; }
        public DateTime trailer_regdate { get; set; }

    }
}
