using Domain.Entities.Hiit;

namespace Domain.DTOs.Hiit
{
    public class HiitDto
    {

        public HiitDto()
        {
            Id = 0;
            Name = string.Empty;
        }

        public HiitDto(HiitEntity entity)
        {
            Id = entity.HiitId;
            Name = entity.HiitDescription;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}