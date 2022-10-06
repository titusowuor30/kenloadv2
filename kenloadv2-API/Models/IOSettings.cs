using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class IOSettings
    {
        [Key]
        public int id { get; set; }
        public String Name { get; set; }
        public String IPAddress { get; set; }
        public bool deckEntry { get; set; }
        public bool deckExit { get; set; }
        public bool WIM { get; set; }
        public String Status { get; set; }

    }
}
