using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Hiit
{
    [Table("hiit_category")]
    public class HiitCategoryEntity
    {

        public HiitCategoryEntity()
        {
            HiitCategoryId = 0;
            CategoryDescription = string.Empty;
        }


        [Column("hiit_category_id")]
        [Key]
        public int HiitCategoryId { get; set; }

        [Column("hiit_category_description")]
        public string CategoryDescription { get; set; }

    }
}