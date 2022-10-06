using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class Penalties
    {
        [Key]
        public int id { get; set; }
        public int points { get; set; }
        public string penalties { get; set; }
    }
}
