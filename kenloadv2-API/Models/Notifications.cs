using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Notifications
    {
        [Key]
        public int id { get; set; }
        public string role { get; set;}
        public string username { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public string readStatus { get; set; }
        public string deletedStatus { get; set; }
    }
}
