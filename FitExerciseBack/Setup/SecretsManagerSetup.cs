using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using FitExerciseBack.Setup.SecretsManager;

namespace FitExerciseBack.Setup
{
    public static class SecretsManagerSetup
    {
        public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder,
                                string region,
                                string secretName,
                                AWSCredentials credentials)
        {
            var configurationSource =
                    new AmazonSecretsManagerConfigurationSource(region, secretName, credentials);

            configurationBuilder.Add(configurationSource);
        }

        public static void LoadSecretsData(this IConfigurationBuilder configurationBuilder,
                        string region,
                        string secretName,
                                AWSCredentials credentials)
        {
            new AmazonSecretsManagerConfigurationSource(region, secretName, credentials).Load(configurationBuilder);


        }
    }
}
