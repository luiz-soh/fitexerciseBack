using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Token.UseCase
{
    public interface ITokenUseCase
    {
        string EncryptPassword(string password);
        string GenerateToken(string name, string role, int validyHours);
    }
}
