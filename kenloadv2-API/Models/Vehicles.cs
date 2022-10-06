using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Vehicles
    {
        [Key]
        public int id { get; set; }
        //[Key]
        [Required(ErrorMessage = "Registration is required")]
        [StringLength (50)]
        public string vehicle_reg { get; set; }

        [StringLength(50)]
        public string vehicle_trailer { get; set; }
        public string vehicle_make { get; set; }
        public string vehicle_model { get; set; }
        public string vehicle_year { get; set; }
        public string vehicle_axle { get; set; }
        public string vehicle_driver { get; set; }
        public string vehicle_tx_code { get; set; }
        public string vehicle_product { get; set; }
        public string vehicle_source { get; set; }
        public string vehicle_destination { get; set; }
        public string vehicle_desc { get; set; }
        public DateTime? vehicle_regdate { get; set; }
        public bool sendsms { get; set; }
        public bool sendmail { get; set; }
        public string vehicle_permit_no { get; set; }
        public DateTime? vehicle_permitissued_date { get; set; }
        public DateTime? vehicle_permitexpiration_date { get; set; }
        public string vehicle_height { get; set; }
        public string vehicle_width { get; set; }
        public string vehicle_length { get; set; }
        public string vehicle_maxweight { get; set; }
        public string vehicle_ownercontact { get; set; }
        public string vehicle_owneremail { get; set; }
        public bool vehicle_deletedstatus { get; set; }
        public int vehicle_demeritpoints { get; set; }


    }
}
