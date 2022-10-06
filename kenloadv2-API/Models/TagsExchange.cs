using System.ComponentModel.DataAnnotations;

namespace KenloadV2API.Models
{
    public class TagsExchange
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string tagid { get; set; }
        [StringLength(50)]
        public string station { get; set; }
        public int status { get; set; }

    }
}
