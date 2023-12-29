using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

namespace FitExerciseBack.Setup.SecretsManager
{
    public class AmazonSecretsManagerConfigurationSource : IConfigurationSource
    {
        private readonly string _region;
        private readonly string _secretName;
        private readonly AWSCredentials _credentials;

        public AmazonSecretsManagerConfigurationSource(string region, string secretName, AWSCredentials credentials)
        {
            _region = region;
            _secretName = secretName;
            _credentials = credentials;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AmazonSecretsManagerConfigurationProvider(_region, _secretName, _credentials);
        }

        public void Load(IConfigurationBuilder builder)
        {
            new AmazonSecretsManagerConfigurationProvider(_region, _secretName, _credentials).Load();
        }
    }
}
