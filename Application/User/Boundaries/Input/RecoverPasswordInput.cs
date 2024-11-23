namespace Application.User.Boundaries.Input
{
    public class RecoverPasswordInput
    {
        public string RecoverCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}