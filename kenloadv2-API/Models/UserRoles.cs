using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class UserRoles
    {
        [Key]
        public int id { get; set; }
        public string userrole_name { get; set; }
        public string userrole_desc { get; set; }
        public bool userrole_deletedstatus { get; set; }
        //public ICollection<Screens>? screens { get; set; }
        public string screens { get; set; }
        public DateTime userrole_timestamp { get; set; }
    }
}
