using System;
using System.ComponentModel.DataAnnotations;

namespace KenloadV2API.Models
{
    public class DataExchange

    {
        [Key]
        public int id { get; set; }
        public int weighbridgetickets { get; set; }
        public string connectionType { get; set; }
        public string server_address { get; set; }
        public int server_port { get; set; }
        public string server_email { get; set; }
        public string server_password { get; set; }
        public string client_address { get; set; }
        public string client_email { get; set; }
        public string client_password { get; set; }
        public int tags { get; set; }
        public DateTime timestamp { get; set; }
    }
}
