using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class CaseResults
    {
        [Key]
        public int id { get; set; }
        public string casereid     { get; set; }
        public string caseid { get; set; }
        public double? drivercharge { get; set; }
        public double? ownercharge { get; set; }
        public double? transportercharge { get; set; }
        public double? loadercharge { get; set; }
        public double? parkingfee { get; set; }
        public string casebasis { get; set; }
        public string receiptno { get; set; }
        public string reason { get; set; }
        public DateTime? datetime { get; set; }
        public DateTime? timestamp { get; set; }
        public string createdby { get; set; }
        
        public string casereticketno { get; set; }
        public string casereregno { get; set; }
        public int deleted { get; set; }
        public string deletedby { get; set; }
        public DateTime? deleteddatetime { get; set; }

        public int casedetailsid { get; set; }



    }
}
