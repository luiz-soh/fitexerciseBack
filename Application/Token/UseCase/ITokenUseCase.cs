namespace Application.Token.UseCase
{
    public interface ITokenUseCase
    {
        string EncryptPassword(string password);
        string GenerateToken(string name, string role, int validyHours);
    }
}
