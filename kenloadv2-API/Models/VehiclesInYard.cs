using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class VehiclesInYard
    {
        [Key]
        public int id { get; set; }
        public string yard_reg { get; set; }
        public string yard_status { get; set; }
        public bool released { get; set; }
        public string request { get; set; }
        public int user_id { get; set; }
        public DateTime entry_date { get; set; }
        public ICollection<WeighbridgeTransactions> wbrg_tickt_no { get; set; }
        public string specialrelease { get; set; }
        public string specialrelease_no { get; set; }
        public string wbrg_name  { get; set; }
        public string wbrg_yard { get; set; }
        public string case_id { get; set; }
        public string yard_timeout { get; set; }

    }
}
