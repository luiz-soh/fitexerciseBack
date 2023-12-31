﻿namespace Domain.DTOs.Gym
{
    public class CreateGymDto
    {
        public CreateGymDto()
        {
            GymName = string.Empty;
            PlanId = 0;
            Password = string.Empty;
            Login = string.Empty;
            Email = string.Empty;
        }
        public CreateGymDto(string gymName, int planId, string password,
         string login, string email)
        {
            GymName = gymName;
            PlanId = planId;
            Password = password;
            Login = login;
            Email = email;
        }

        public string GymName { get; set; }
        public int PlanId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
