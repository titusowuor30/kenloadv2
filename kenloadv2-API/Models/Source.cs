using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Source
    {
        [Key]
        public int id { get; set; }
        public string source_code { get; set; }
        public string source_name { get; set; }
        public string source_deletedstatus { get; set; }
        public string source_regdate { get; set; }

    }
}
