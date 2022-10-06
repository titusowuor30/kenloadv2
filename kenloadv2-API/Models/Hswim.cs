using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Hswim
    {
        [Key]
        public int id { get; set; }
        public int hswim_recordserialno { get; set; }
        public string station { get; set; }
        public string axles { get; set; }
        public string anpr { get; set; }
        public string anprassist { get; set; }
        public string axleconfig { get; set; }
        public double speed { get; set; }
        public double axle1 { get; set; }
        public double axle2 { get; set; }
        public double axle3 { get; set; }
        public double axle4 { get; set; }
        public double axle5 { get; set; }
        public double axle6 { get; set; }
        public double axle7 { get; set; }
        public double axle8 { get; set; }
        public double perm1 { get; set; }
        public double perm2 { get; set; }
        public double perm3 { get; set; }
        public double perm4 { get; set; }
        public double perm5 { get; set; }
        public double perm6 { get; set; }
        public double perm7 { get; set; }
        public double perm8 { get; set; }
        public double lngth1 { get; set; }
        public double lngth2 { get; set; }
        public double lngth3 { get; set; }
        public double lngth4 { get; set; }
        public double lngth5 { get; set; }
        public double lngth6 { get; set; }
        public double lngth7 { get; set; }
        public double lngth8 { get; set; }
        public double avw { get; set; }
        public double gvw { get; set; }
        public double pernmissible { get; set; }
        public DateTime timestamp { get; set; }
        public bool calledin { get; set; }
        public bool anprchecked { get; set; }
        public string wbrg_ticket_no { get; set; }
        public bool wimtagged { get; set; }
        public string wintagreason { get; set; }
        public string anprassistreason { get; set; }
        public int lane { get; set; }
        public int type { get; set; }
        public bool wimtagedcheck { get; set; }
        public int gap { get; set; }
        public string calledinreason { get; set; }
        public int MyProperty { get; set; }
        public string wbrg_ticket_exported { get; set; }

    }
}
