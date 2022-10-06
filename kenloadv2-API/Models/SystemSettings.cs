using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class SystemSettings
    {
        [Key]
        public int id { get; set; }
        public string stationcode { get; set; }

        public string stationcode2 { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string hswim { get; set; }

        public string camera { get; set; }
        //ALTER TABLE `systemsettings` ADD COLUMN `camera` VARCHAR(50) NULL DEFAULT NULL AFTER `hswim`;
    }
}
