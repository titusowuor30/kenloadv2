using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class BillingInstruction
    {
        [Key]
        public int id { get; set; }
        public string billingid { get; set; }
        public string caseid { get; set; }
        public string billingfrom { get; set; }
        public string billingto { get; set; }
        public DateTime? datetime { get; set; }
        public int absconding { get; set; }
        public DateTime? abscondingdatetime { get; set; }
        public int parking { get; set; }
        public DateTime? parkingfromdatetime { get; set; }
        public DateTime? parkingtodatetime { get; set; }
        public int overload { get; set; }
        public int others { get; set; }
        public string otherdetails { get; set; }
        public double? payableamount { get; set; }

        public DateTime? timestamp { get; set; }      
        public string createdby { get; set; }
        public int deleted { get; set; }
        public string deletedby { get; set; }
        public DateTime? deleteddatetime { get; set; }
        public string verifiedby { get; set; }
        public DateTime? verifieddatetime { get; set; }
        public int casedetailsid { get; set; }




    }
}
