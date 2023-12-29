namespace Domain.DTOs.Gym
{
    public class GymTokenDto
    {
        public GymTokenDto()
        {
            GymId = 0;
            Token = string.Empty;
            GymName = string.Empty;
        }

        public GymTokenDto(int gymId, string gymName, string token)
        {
            GymId = gymId;
            GymName = gymName;
            Token = token;
        }

        public int GymId { get; set; }
        public string GymName { get; set; }
        public string Token { get; set; }
    }
}