using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class BackupLogs
    {
        [Key]
        public string backuptype { get; set; }
        public string filename { get; set; }
        public string folderpath { get; set; }
        public string daysofweek { get; set; }
        public DateTime timestamp { get; set; }

    }
}
