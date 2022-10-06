using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class UploadFile
    {
        [Key]
        public int id { get; set; }
        public string imageName { get; set; }
        public string? imageData { get; set; }
        public DateTime uploadedDate { get; set; }
        public string location { get; set; }
        public string? Status { get; set; }
    }
}
