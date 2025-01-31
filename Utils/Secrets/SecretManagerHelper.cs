// Utils/Secrets/SecretManagerHelper.cs
namespace FortraCountLicenses.Utils.Secrets;
using System;
using Google.Cloud.SecretManager.V1;
using Newtonsoft.Json;
using FortraCountLicenses.Utils.GCP;

public class SecretManagerHelper
{
    public static (string fortraAccountId, string fortraAuthToken, string howToAccessSecrets) AccessSecrets()
    {
        string logHeadline = "SecretManagerHelper·AccessSecrets·";
        Console.WriteLine($"{logHeadline} Howto access secret --------------------------------------");

        string? howToAccessSecrets = null;
        string? fortraAccountId = null;
        string? fortraAuthToken = null;

        // Try to get project ID from environment variable
        string? googleCloudProjectId = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT");
        
        // Access via GCP Secret Manager
        if (!string.IsNullOrEmpty(googleCloudProjectId))
        {
            Console.WriteLine($"{logHeadline} Accessing Google Cloud Secret Manager");
            howToAccessSecrets = "Google Cloud Secret Manager";

            var secretReader = new GoogleSecretManagerReader();
            string mySecret = secretReader.ReadSecret("fortra-count-licenses");

            // Deserialize and extract information from secret
            SecretData? secretData = JsonConvert.DeserializeObject<SecretData>(mySecret);
            if (secretData == null || string.IsNullOrEmpty(secretData.FortraAccountId) || secretData.GmailerGoogleServiceAccount == null)
            {
                throw new Exception($"{logHeadline} Error: Secret data could not be deserialized or is missing required information.");
            }

            fortraAccountId = secretData.FortraAccountId;
            fortraAuthToken = secretData.FortraAuthToken;
        }
        else
        {
            // Access via environment variables directly
            fortraAccountId = Environment.GetEnvironmentVariable("FORTRA_ACCOUNT_ID");
            fortraAuthToken = Environment.GetEnvironmentVariable("FORTRA_AUTH_TOKEN");

            if (string.IsNullOrEmpty(fortraAccountId) || string.IsNullOrEmpty(fortraAuthToken))
            {
                throw new InvalidOperationException($"{logHeadline} Missing Env var ACCOUNT_ID");
            }
            else
            {
                howToAccessSecrets = "Environment variables";
            }
        }

        if (string.IsNullOrEmpty(howToAccessSecrets))
        {
            throw new Exception($"{logHeadline} howToAccessSecrets not found.");
        }

        return (fortraAccountId, fortraAuthToken, howToAccessSecrets);
    }
}
