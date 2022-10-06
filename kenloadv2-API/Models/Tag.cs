using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class Tag
    {
        [Key]
        public int id { get; set; }
        public string taguid { get; set; }
        public string vehreg { get; set; }
        public string reason { get; set; }
        public DateTime datetime { get; set; }
        public string userid { get; set; }
        public string station { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public string sms { get; set; }
        public string email { get; set; }
        public string removedby { get; set; }
        public DateTime removeddatetime { get; set; }
        public string removedreason { get; set; }
        public string exported { get; set; }

    }
}
