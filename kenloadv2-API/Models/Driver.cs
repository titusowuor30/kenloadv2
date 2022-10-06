using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class Driver
    {
        [Key]
        public int id { get; set; }
        public string license_no { get; set; }
        public string driver_name { get; set; }
        public string driver_dob  { get; set; }
        public string  driver_gender { get; set; }
        public string driver_race  { get; set; }
        public string driver_image  { get; set; }
        public string driver_emailaddress  { get; set; }
        public string driver_pysicaladdress { get; set; }
        public string driver_town { get; set; }
        public string driver_postaladdress { get; set; }
        public string driver_contact { get; set; }
        public string driver_idno { get; set; }
        public string driver_nationality { get; set; }
        public int driver_age { get; set; }
        public string driver_physcaldescription { get; set; }
        public bool  driver_deletedstatus { get; set; }
        public  DateTime driver_regdate { get; set; }
    }

}
