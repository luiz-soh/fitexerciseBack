using Domain.Entities.Hiit;

namespace Domain.DTOs.Hiit
{
    public class HiitSerieDto
    {

        public HiitSerieDto()
        {
            Id = 0;
            Step = string.Empty;
        }

        public HiitSerieDto(HiitSerieEntity entity)
        {
            Id = entity.SerieId;
            Step = entity.SerieStep;
        }

        public int Id { get; set; }
        public string Step { get; set; }
    }
}