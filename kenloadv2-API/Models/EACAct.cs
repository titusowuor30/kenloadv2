using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class EACAct
    {
        [Key]
        public int id { get; set; }
        public string caseid { get; set; }
        public DateTime datetime { get; set; }
        public string casedocid { get; set; }
        public double gvwfee { get; set; }
        public double axlefee { get; set; }
        public int demeritspoints { get; set; }
        public int cdemeritspoints { get; set; }
        public string penalties { get; set; }
        public double dollarrate { get; set; }
        public string act { get; set; }

    }
}
