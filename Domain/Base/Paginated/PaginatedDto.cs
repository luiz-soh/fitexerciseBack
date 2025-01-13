namespace Domain.Base.Paginated
{
    public class PaginatedDto
    {
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public string? Orderby { get; set; }
        public string? Order { get; set; }
        public string? Search { get; set; }
    }
}