using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class CaseDetails
    {
        [Key]
        public int id { get; set; }
        public string caseid     { get; set; }
        public string station { get; set; }
        public string caseticket { get; set; }
        public string caseregno { get; set; }
        public Double caseexcessload { get; set; }
        public string driverid { get; set; }
        public string driverslicense { get; set; }
        public string drivername { get; set; }
        public string driversurname { get; set; }
        public string drivergender { get; set; }
        public string drivernationality { get; set; }
        public int? driverage { get; set; }
        public string driveraddress { get; set; }
        public string alongroad { get; set; }
        public string district { get; set; }
        public string weighbrige { get; set; }
        public string county { get; set; }
        public string court { get; set; }
        public string comppolice { get; set; }
        public string trafficofficer { get; set; }
        public string repeatedoffence { get; set; }
        public string vehicleins { get; set; }
        public string movevehicleto { get; set; }

        public string repairsneeded { get; set; }
        public string offloadreg { get; set; }
        public string transportername { get; set; }
        public string transporteradd { get; set; }
        public string vehiclemake { get; set; }
        public DateTime? weighdate { get; set; }
        public string tickettransporter { get; set; }
        public string ticketaxletype { get; set; }
        public DateTime? casedatetime { get; set; }
        public string createdby { get; set; }
        public string editedby { get; set; }
        public DateTime? editeddate { get; set; }
        public string WBT_operator { get; set; }
        public string chargedincourt { get; set; }
        public string ownerid { get; set; }
        public string ownername { get; set; }
        public string ownergender { get; set; }
        public string ownernationality { get; set; }
        public int? ownerage { get; set; }
        public string ownersurname { get; set; }
        public string owneraddress { get; set; }
        public string prohibitionorder { get; set; }
        public string dl { get; set; }
        public string transporterno { get; set; }
        public ICollection<CaseDocs> casedocs { get; set; }

        public ICollection<CaseResults> caseresults { get; set; }
        public ICollection<BillingInstruction> billinginstruction { get; set; }

        public int? paystation { get; set; }

        public string permitno { get; set; }


    }
}
