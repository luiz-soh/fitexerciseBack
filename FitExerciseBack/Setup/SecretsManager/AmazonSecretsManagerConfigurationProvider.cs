using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text;
using System.Text.Json;

namespace FitExerciseBack.Setup.SecretsManager
{
    public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;
        private readonly string _accessKey;
        private readonly string _secretKey;


        public AmazonSecretsManagerConfigurationProvider(string region, string secretName, string accessKey, string secretKey)
        {
            _region = region;
            _secretName = secretName;
            _accessKey = accessKey;
            _secretKey = secretKey;
        }

        public override void Load()
        {
            var secret = GetSecret();

            if (!string.IsNullOrEmpty(secret))
            {
                Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret)!;
            }
        }

        private string GetSecret()
        {
            using var client =
            new AmazonSecretsManagerClient(
                _accessKey,
                _secretKey,
                RegionEndpoint.GetBySystemName(_region));

            var request = new GetSecretValueRequest
            {
                SecretId = _secretName,
                VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
            };

            var response = client.GetSecretValueAsync(request).Result;

            string secretString;
            if (response.SecretString != null)
            {
                secretString = response.SecretString;
            }
            else
            {
                var memoryStream = response.SecretBinary;
                var reader = new StreamReader(memoryStream);
                secretString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            }

            return secretString;
        }
    }
}
