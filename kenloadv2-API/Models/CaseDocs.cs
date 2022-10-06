using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
      public class CaseDocs
    {
        [Key]
        public int id { get; set; }
        public string casedocid { get; set; }
        public string caseid { get; set; }
        public string docname { get; set; }
       // public DateTime wbrg_ticket_date { get; set; }
       // public DateTime docdate { get; set; }
        public DateTime docdate
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;
        public string createdby { get; set; }
        public string editedy { get; set; }
        public string cancelled { get; set; }
        public string cancelledby { get; set; }
        public string printed { get; set; }
        public string poorder { get; set; }
        public DateTime? editeddate { get; set; }
        public int casedetailsid { get; set; }
        //public ICollection<CaseDetails> CaseDetails { get; set; }
    }
}
