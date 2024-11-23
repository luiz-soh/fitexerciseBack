namespace Domain.Configuration
{
    public class Secrets
    {
        public Secrets()
        {
            FitExerciseDB = string.Empty;
            ClientSecret = string.Empty;
            PreSalt = string.Empty;
            PosSalt = string.Empty;
            S3Url = string.Empty;
            SendGridApiKey = string.Empty;
            FromAddress = string.Empty;
            FromName = string.Empty;
        }

        public string FitExerciseDB { get; set; }
        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
        public string S3Url { get; set; }
        public string SendGridApiKey { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
}
