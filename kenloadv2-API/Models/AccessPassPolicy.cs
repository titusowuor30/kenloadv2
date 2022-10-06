using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class AccessPassPolicy
    {
        [Key]
        public int id { get; set; }
        public int app_char { get; set; }
        public int app_day_exp { get; set; }
        public int app_capital { get; set; }
        public int app_small { get; set; }
        public int app_numbers { get; set; }
        public int app_logoff_period { get; set; }
        public int app_min_char { get; set; }
    }
}
