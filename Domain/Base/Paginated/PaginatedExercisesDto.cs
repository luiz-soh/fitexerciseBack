namespace Domain.Base.Paginated
{
    public class PaginatedDto<T>
    {
        public PaginatedDto()
        {
            Total = 0;
            ObjectDto = [];
        }

        public PaginatedDto(int total, List<T>? objectDto)
        {
            Total = total;
            ObjectDto = objectDto ?? [];
        }

        public int Total { get; set; }
        public List<T> ObjectDto { get; set; }
    }
}