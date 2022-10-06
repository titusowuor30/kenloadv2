using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Screens
    {
        [Key]
        public int id { get; set; }
        public string  screen_name { get; set; }
        public int active { get; set; }
    }
}
