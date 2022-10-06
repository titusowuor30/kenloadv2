﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class DollarRate
    {
        [Key]
        public int id { get; set; }
        public double rate { get; set; }

        public DateTime datetime { get; set; }
        public string username { get; set; }

    }
}
