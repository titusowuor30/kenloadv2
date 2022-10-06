using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class VirtualTicket
    {
        [Key]
        public int id { get; set; }
        public int cid { get; set; }
        public DateTime date_time_local { get; set; }
        public string wim { get; set; }
        public int vehicle_class { get; set; }
        public int velocity { get; set; }
        public int length { get; set; }
        public string lp { get; set; }
        public string anprassist { get; set; }
        public string lp_back { get; set; }
        public DateTime insert_timestamp { get; set; }
        public int total_weight { get; set; }
        public int axles_count { get; set; }
        public int axle_1_load { get; set; }
        public int axle_2_load { get; set; }
        public int axle_3_load { get; set; }
        public int axle_4_load { get; set; }
        public int axle_5_load { get; set; }
        public int axle_6_load { get; set; }
        public int axle_7_load { get; set; }
        public int axle_8_load { get; set; }
        public int axle_9_load { get; set; }
        public int axle_1_leftside_load { get; set; }
        public int axle_2_leftside_load { get; set; }
        public int axle_3_leftside_load { get; set; }
        public int axle_4_leftside_load { get; set; }
        public int axle_5_leftside_load { get; set; }
        public int axle_6_leftside_load { get; set; }
        public int axle_7_leftside_load { get; set; }
        public int axle_8_leftside_load { get; set; }
        public int axle_9_leftside_load { get; set; }
        public int axle_1_rightside_load { get; set; }
        public int axle_2_rightside_load { get; set; }
        public int axle_3_rightside_load { get; set; }
        public int axle_4_rightside_load { get; set; }
        public int axle_5_rightside_load { get; set; }
        public int axle_6_rightside_load { get; set; }
        public int axle_7_rightside_load { get; set; }
        public int axle_8_rightside_load { get; set; }
        public int axle_9_rightside_load { get; set; }
        public string axle_conf { get; set; }
        public int Axl_Legal_Weight_1 { get; set; }
        public int Axl_Legal_Weight_2 { get; set; }
        public int Axl_Legal_Weight_3 { get; set; }
        public int Axl_Legal_Weight_4 { get; set; }
        public int Axl_Legal_Weight_5 { get; set; }
        public int Axl_Legal_Weight_6 { get; set; }
        public int Axl_Legal_Weight_7 { get; set; }
        public int Axl_Legal_Weight_8 { get; set; }
        public int Axl_Legal_Weight_9 { get; set; }
        public int permissible { get; set; }
        public int AVW { get; set; }
        public int GVW { get; set; }
        public string overweighting { get; set; }
        public string dual_tire { get; set; }
        public string car_layout { get; set; }
        public int axle_overload_no { get; set; }
        public int tagged { get; set; }

        public int exported { get; set; }
        public int calledin { get; set; }
        public string calledinreason { get; set; }
        public string wbrg_ticket_no { get; set; }
        public int gap { get; set; }
        public string lp_image_front { get; set; }
        public string lp_image_back { get; set; }
        public string overview { get; set; }





    }
}
