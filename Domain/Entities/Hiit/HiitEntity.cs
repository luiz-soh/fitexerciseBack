using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Hiit
{
    [Table("hiit")]
    public class HiitEntity
    {
        public HiitEntity()
        {
            HiitId = 0;
            HiitCategoryId = 0;
            HiitDescription = string.Empty;
            HiitPosition = 0;
        }


        [Column("hiit_id")]
        [Key]
        public int HiitId { get; set; }

        [Column("hiit_category_id")]
        public int HiitCategoryId { get; set; }


        [Column("hiit_description")]
        public string HiitDescription { get; set; }

        [Column("hiit_position")]
        public int HiitPosition { get; set; }
    }
}