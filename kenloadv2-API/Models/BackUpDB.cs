using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class BackUpDB
    {
        [Key]
        public int id { get; set; }
        public string backup_time { get; set; }
        public int backup_m { get; set; }   
        public int backup_t { get; set; }   
        public int backup_w { get; set; }   
        public int backup_th { get; set; }  
        public int backup_f { get; set; }   
        public int backup_s { get; set; }
        public int backup_su { get; set; }  
        public String backup_name { get; set; }
        public String backup_path { get; set; }
        public int delete_status { get; set; }
        public string fullincr { get; set; }

    }
}
