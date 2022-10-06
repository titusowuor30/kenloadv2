using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class ScaleTest
    {
        [Key]
        public int id { get; set; }
        public string scaleid { get; set; }
        [StringLengthAttribute(50)]
        public string station { get; set; }
        [StringLengthAttribute(1)]
        public string scalename { get; set; }
        public string scalevehreg { get; set; }
        public Double scaledeck1 { get; set; }
        public Double scaledeck2 { get; set; }
        public Double scaledeck3 { get; set; }
        public Double scaledeck4 { get; set; }
        public DateTime scaledatetime { get; set; }
        public string scaleweighedby { get; set; }
        public string result { get; set; }

    }
}