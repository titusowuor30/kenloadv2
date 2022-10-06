using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class DeckWeights
    {
        [Key]
        public string wbrg_name  { get; set; }
        public double deck_1 { get; set; }
        public double deck_2 { get; set; }
        public double deck_3 { get; set; }
        public double deck_4 { get; set; }
        public double total { get; set; }


    }
}
