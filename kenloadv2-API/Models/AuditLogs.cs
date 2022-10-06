using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AuditLogs
    {
        [Key]
        public int id { get; set; }
        public DateTime datetime { get; set; }
        public string useremail { get; set; }
        public string application { get; set; }
        public string details { get; set; }
        public string computer { get; set; }

    }
}
