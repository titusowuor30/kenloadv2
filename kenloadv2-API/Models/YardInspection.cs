using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class YardInspection
    {
        [Key]
        public int id { get; set; }
        public string inspectionid { get; set; }
        public string registration { get; set; }
        public string traillerreg { get; set; }
        public string transp { get; set; }
        public string transptel { get; set; }
        public string transpadd { get; set; }
        public string model { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string Axleconf { get; set; }
        public string FirstAkit { get; set; }
        public string Fireext { get; set; }
        public string DrunkDriver { get; set; }
        public string Dlicensestate { get; set; }
        public string Inspstick { get; set; }
        public string InsuaranceStic { get; set; }
        public string Loaddisc { get; set; }
        public string Insecureload { get; set; }
        public string Lightstatus { get; set; }
        public string Sterringdef { get; set; }
        public string FHBreak { get; set; }
        public string AODefects { get; set; }
        public string AbnormalLPermit { get; set; }
        public string remarks { get; set; }
        public DateTime Dpermitissu { get; set; }
        public DateTime PStartD { get; set; }
        public DateTime PEndD { get; set; }
        public DateTime DateofTravel { get; set; }
        public Double height_ { get; set; }
        public Double length_ { get; set; }
        public Double width_ { get; set; }
        public Double weighofload { get; set; }
        public Double Authweight { get; set; }
        public int deletestatus { get; set; }
        public DateTime datetime { get; set; }
        public string status { get; set; }
        public int Totaltyres { get; set; }
        public int perconf { get; set; }
        public int nonperconf { get; set; }
        public String station { get; set; }
        public String SteeringOL { get; set; }
        public String SteeringBP { get; set; }
        public String rodend { get; set; }
        public String balljoints { get; set; }
        public String wheelallignment { get; set; }

        public String ticketno { get; set; }


    }
}
