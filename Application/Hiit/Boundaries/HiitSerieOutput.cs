using Domain.DTOs.Hiit;

namespace Application.Hiit.Boundaries
{
        public class HiitSerieOutput
    {

        public HiitSerieOutput()
        {
            Id = 0;
            Step = string.Empty;
        }

        public HiitSerieOutput(HiitSerieDto dto)
        {
            Id = dto.Id;
            Step = dto.Step;
        }

        public int Id {get;set;}
        public string Step { get; set; }
    }
}