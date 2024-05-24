using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Hiit
{
    [Table("hiit_serie")]
    public class HiitSerieEntity
    {
        public HiitSerieEntity()
        {
            SerieId = 0;
            HiitId = 0;
            SerieStep = string.Empty;
        }


        [Column("serie_id")]
        [Key]
        public int SerieId { get; set; }

        [Column("hiit_id")]
        public int HiitId { get; set; }


        [Column("serie_step")]
        public string SerieStep { get; set; }
    }
}