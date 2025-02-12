using Domain.Base.Paginated;
using Domain.DTOs.User;

namespace Application.User.Boundaries.Output
{
    public class PaginatedUsersOutput
    {

        public int Total { get; set; }
        public List<UserDto> Users { get; set; }

        public PaginatedUsersOutput(PaginatedDto<UserDto> dto)
        {
            Total = dto.Total;
            Users = dto.ObjectDto;
        }

        public PaginatedUsersOutput()
        {
            Total = 0;
            Users = [];
        }
    }
}