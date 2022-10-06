using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Models
{
    public class WeighbridgeTransactions
    {
        [Key]
        public int id { get; set; }
        public string wbrg_ticket_no { get; set; }
        public string wbrg_ticket_type { get; set; }
        public string wbrg_station { get; set; }
        public string wbrg_roadauthority { get; set; }
        public string wbrg_ticket_weighbridge { get; set; }
        public string wbrg_ticket_source { get; set; }
        public string wbrg_ticket_destination { get; set; }
        public DateTime wbrg_ticket_date { get; set; }
        public DateTime wbrg_ticket_dateout { get; set; }
        public string wbrg_product { get; set; }
        public string wbrg_tx_code { get; set; }
        public string veh_reg { get; set; }
        public string anpr_veh_reg { get; set; }
        public double axle1_weight { get; set; }
        public double axle1_limit { get; set; }
        public DateTime? axle1_time { get; set; }
        public string axle1_group_type1 { get; set; }
        public string axle1_group_type2 { get; set; }
        public double axle2_weight { get; set; }
        public double axle2_limit { get; set; }
        public DateTime? axle2_time { get; set; }
        public string axle2_group_type1 { get; set; }
        public string axle2_group_type2 { get; set; }
        public double axle3_weight { get; set; }
        public double axle3_limit { get; set; }
        public DateTime? axle3_time { get; set; }
        public string axle3_group_type1 { get; set; }
        public string axle3_group_type2 { get; set; }
        public double axle4_weight { get; set; }
        public double axle4_limit { get; set; }
        public DateTime? axle4_time { get; set; }
        public string axle4_group_type1 { get; set; }
        public string axle4_group_type2 { get; set; }
        public double axle5_weight { get; set; }
        public double axle5_limit { get; set; }
        public DateTime? axle5_time { get; set; }
        public string axle5_group_type1 { get; set; }
        public string axle5_group_type2 { get; set; }
        public double axle6_weight { get; set; }
        public double axle6_limit { get; set; }
        public DateTime? axle6_time { get; set; }
        public string axle6_group_type1 { get; set; }
        public string axle6_group_type2 { get; set; }
        public double axle7_weight { get; set; }
        public double axle7_limit { get; set; }
        public DateTime? axle7_time { get; set; }
        public string axle7_group_type1 { get; set; }
        public string axle7_group_type2 { get; set; }
        public double axle8_weight { get; set; }
        public double axle8_limit { get; set; }
        public DateTime? axle8_time { get; set; }
        public string axle8_group_type1 { get; set; }
        public string axle8_group_type2 { get; set; }
        public double wbrg_ticket_grossweight { get; set; }
        public double wbrg_ticket_tareweight { get; set; }
        public double wbrg_ticket_stamp { get; set; }
        public string wbrg_ticket_operator { get; set; }
        public string wbrg_ticket_status { get; set; }
        public string wbrg_ticket_state { get; set; }
        public string wbrg_ticket_printed { get; set; }
        public string wbrg_ticket_direction { get; set; }
        public string wbrg_ticket_directionarea { get; set; }
        public string wbrg_ticket_axel { get; set; }
        public string wbrg_ticket_ref { get; set; }
        public double wbrg_ticket_excessweight { get; set; }
        public string wbrg_ticket_driver { get; set; }
        public string wbrg_ticket_shift { get; set; }
        public string wbrg_ticket_wim { get; set; }
        public string wbrg_ticket_notes { get; set; }
        public string wbgr_ticket_trailer_no { get; set; }
        public string wbrg_ticket_excessload { get; set; }
        public int yardlistid { get; set; }
        public string wbrg_ticket_case_id { get; set; }
        public int wbrg_ticket_reweigh_no { get; set; }
        public int wbrg_ticket_tagged { get; set; }
        public string wbrg_ticket_charge_no { get; set; }
        public double wbrg_ticket_axleload { get; set; }
        public double wbrg_ticket_gvwload { get; set; }
        public string permit_no { get; set; }
        public string wbrg_ticket_make { get; set; }
        public string reliefvehreg { get; set; }
        public int exported { get; set; }
        public ICollection<YardList> yardList { get; set; }

    }
}
