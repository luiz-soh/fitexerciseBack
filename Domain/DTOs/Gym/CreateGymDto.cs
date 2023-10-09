using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Gym
{
    public class CreateGymDto
    {
        //public CreateGymDto(CreateGymInput input, string encryptedPassword)
        //{
        //    GymName = input.GymName;
        //    PlanId = input.PlanId;
        //    Password = encryptedPassword;
        //    Login = input.Login;
        //    Email = input.Email;
        //}

        public string GymName { get; set; }
        public int PlanId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
