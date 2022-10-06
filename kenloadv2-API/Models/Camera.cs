using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class Camera
    {
        [Key]
        public int id { get; set; }
        public string position { get; set; }
        public string weighbridge { get; set; }
        public string cameraname { get; set; }
        public string ipaddress { get; set; }
        public int port { get; set; }
        public string file { get; set; }
        public string protocol { get; set; }

    }
}
