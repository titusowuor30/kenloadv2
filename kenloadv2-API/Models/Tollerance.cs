using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Tollerance
    {
        [Key]
        public int id { get; set; }
        public double singleaxle { get; set; }
        public double groupaxle { get; set; }
        public double gvw { get; set; }
        public string username { get; set; }
        public DateTime datetime { get; set; }
    }
}
