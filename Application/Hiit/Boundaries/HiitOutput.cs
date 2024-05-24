using Domain.DTOs.Hiit;

namespace Application.Hiit.Boundaries
{
    public class HiitOutput
    {

        public HiitOutput()
        {
            Id = 0;
            Name = string.Empty;
        }

        public HiitOutput(HiitDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}