using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class SpecialRelease
    {
        [Key]
        public int id { get; set; }
        public string specialid { get; set; }
        public string vehiclereg { get; set; }
        public string  wbrg_ticket_no { get; set; }
        public string reason { get; set; }
        public string otherreason { get; set; }
        public DateTime realeasedate { get; set; }
        public string realeasedby { get; set; }
        public double excessload { get; set; }
        public String case_document_id { get; set; }
        public int specialrelease_status { get; set; }
        public string station { get; set; }

    }
}
