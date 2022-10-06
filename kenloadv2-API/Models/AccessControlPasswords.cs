using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AccessControlPasswords
    {
        [Key]
        public int id { get; set; }
        public int screenid { get; set; }
        public string acp_pass { get; set; }
        public string username { get; set; }
        public string level { get; set; }
    }
}
