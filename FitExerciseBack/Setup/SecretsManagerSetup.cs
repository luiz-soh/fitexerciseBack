using FitExerciseBack.Setup.SecretsManager;

namespace FitExerciseBack.Setup
{
    public static class SecretsManagerSetup
    {
        public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder,
                                string region,
                                string secretName,
                                string accessKey,
                                string secretKey)
        {
            var configurationSource =
                    new AmazonSecretsManagerConfigurationSource(region, secretName, accessKey, secretKey);

            configurationBuilder.Add(configurationSource);
        }

        public static void LoadSecretsData(this IConfigurationBuilder configurationBuilder,
                        string region,
                        string secretName,
                        string accessKey,
                        string secretKey)
        {
            new AmazonSecretsManagerConfigurationSource(region, secretName, accessKey, secretKey).Load(configurationBuilder);


        }
    }
}
