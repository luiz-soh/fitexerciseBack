using Domain.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Token.UseCase
{
    public class TokenUseCase : ITokenUseCase
    {
        private readonly Secrets _secrets;

        public TokenUseCase(IOptions<Secrets> secrets)
        {
            _secrets = secrets.Value;
        }

        public string EncryptPassword(string dataToEncrypt)
        {
            string encryptedData;
            var bytes = Encoding.UTF8.GetBytes($"{_secrets.PreSalt}{dataToEncrypt}{_secrets.PosSalt}");
            var hash = SHA512.HashData(bytes);
            encryptedData = GetStringFromHash(hash);

            return encryptedData;
        }

        public string GenerateToken(string name, string role, int validyHours, int userId, int gymId = 0)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _secrets.ClientSecret;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role)
                ]),
                Expires = DateTime.UtcNow.AddHours(validyHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (gymId > 0)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Actor, gymId.ToString()));

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string GenerateRecoveryCode()
        {
            const string characters = "1234567890";
            string otp = string.Empty;
            Random random = new();

            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(0, characters.Length);
                otp += characters[index];
            }

            return otp;
        }

        #region private methods
        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        #endregion
    }
}
