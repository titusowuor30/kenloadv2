using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string  username { get; set; }

        [StringLength(150)]
        public string fullnames { get; set; }

        [StringLength(150)]
        public string organization { get; set; }
        
        //public string userpass { get; set; }
        //public ICollection<UserRoles>? userrole { get; set; }
        public string UserRole { get; set; }
        // public string screens { get; set; }
        public bool deletedstatus { get; set; }
        public DateTime?  timestamp { get; set; }
        
        [StringLength(50)]
        public string  email { get; set; }
       
        [StringLength(50)]
        public string phone { get; set; }
        
        [StringLength(50)]
        public string sendmail { get; set;}

        [StringLength(50)]
        public string sendsms { get; set; }
        public DateTime? resettime { get; set; }
        
    }
}
