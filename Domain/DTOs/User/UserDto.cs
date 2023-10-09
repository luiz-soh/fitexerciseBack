using Domain.Entities.User;

namespace Domain.DTOs.User
{
    public class UserDto
    {
        public UserDto(FitUser user)
        {
            Id = user.UserId;
            Name = user.Username;
            Email = user.UserEmail ?? string.Empty;
        }

        public UserDto()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
