using Application.User.Boundaries.Input;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.SignIn.Validation
{
    public class SignInValidation : AbstractValidator<SignInInput>
    {
        public SignInValidation() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}
