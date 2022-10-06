using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class UserPass
    {
        [Key]
        public int id { get; set; }        
        public string userpassword { get; set; }  
        public bool expirationstatus { get; set; }
    }
}
