namespace FitExerciseBack.Setup.SecretsManager
{
    public class AmazonSecretsManagerConfigurationSource : IConfigurationSource
    {
        private readonly string _region;
        private readonly string _secretName;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public AmazonSecretsManagerConfigurationSource(string region, string secretName, string accessKey, string secretKey)
        {
            _region = region;
            _secretName = secretName;
            _accessKey = accessKey;
            _secretKey = secretKey;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AmazonSecretsManagerConfigurationProvider(_region, _secretName, _accessKey, _secretKey);
        }

        public void Load(IConfigurationBuilder builder)
        {
            new AmazonSecretsManagerConfigurationProvider(_region, _secretName, _accessKey, _secretKey).Load();
        }
    }
}
