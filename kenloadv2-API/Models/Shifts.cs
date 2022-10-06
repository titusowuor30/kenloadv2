using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Shifts
    {

        [Key]
        public int id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string shift_type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string position { get; set; }
        public bool deletedstatus { get; set; }
        public bool repeat { get; set; }
    }
}
