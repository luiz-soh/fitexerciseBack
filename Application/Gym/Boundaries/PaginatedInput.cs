using Domain.Base.Paginated;

namespace Application.Gym.Boundaries
{
    public class PaginatedInput : PaginatedDto
    {
        public PaginatedInput(int gymId, int? perPage, int? page, string orderby, string order, string? search)
        {
            GymId = gymId;
            PerPage = perPage;
            Page = page;
            Orderby = orderby;
            Order = order;
            Search = search;
        }

        public int GymId { get; set; }
    }
}