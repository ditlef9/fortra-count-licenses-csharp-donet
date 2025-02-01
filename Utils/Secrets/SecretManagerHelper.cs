// Utils/Secrets/SecretManagerHelper.cs
namespace FortraCountLicenses.Utils.Secrets;

using System;
using Google.Cloud.SecretManager.V1;
using Newtonsoft.Json;
using FortraCountLicenses.Utils.GCP;

public class SecretManagerHelper
{
    public static (string fortraAccountId, string fortraAuthToken, string emailFromAddress, string emailFromName, string emailTo, GmailerGoogleServiceAccount gmailerGoogleServiceAccount) AccessSecrets()
    {
        string logHeadline = "SecretManagerHelper·AccessSecrets·";
        Console.WriteLine($"{logHeadline} How to access secret --------------------------------------");

        string? fortraAccountId = null;
        string? fortraAuthToken = null;
        string? emailFromAddress = null;
        string? emailFromName = null;
        string? emailTo = null;
        GmailerGoogleServiceAccount? gmailerGoogleServiceAccount = null;

        // Try to get project ID from environment variable
        string? googleCloudProjectId = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT_ID");
        
        // Access via GCP Secret Manager
        if (!string.IsNullOrEmpty(googleCloudProjectId))
        {
            Console.WriteLine($"{logHeadline} Accessing Google Cloud Secret Manager");

            var secretReader = new GoogleSecretManagerReader();
            string mySecret = secretReader.ReadSecret("fortra-count-licenses");

            // Deserialize and extract information from secret
            SecretData? secretData = JsonConvert.DeserializeObject<SecretData>(mySecret);
            if (secretData == null || string.IsNullOrEmpty(secretData.FortraAccountId))
            {
                throw new Exception($"{logHeadline} Error: Secret data could not be deserialized or is missing required information.");
            }

            fortraAccountId = secretData.FortraAccountId;
            fortraAuthToken = secretData.FortraAuthToken;
            emailFromAddress = secretData.EmailFromAddress;
            emailFromName = secretData.EmailFromName;
            emailTo = secretData.EmailTo;
            gmailerGoogleServiceAccount = secretData.GmailerGoogleServiceAccount;
            if (gmailerGoogleServiceAccount == null)
            {
                throw new Exception($"{logHeadline} Error: Could not get GmailerGoogleServiceAccount from secret data.");
            }

        }
        else
        {
            // Access via environment variables directly
            fortraAccountId = Environment.GetEnvironmentVariable("FORTRA_ACCOUNT_ID");
            fortraAuthToken = Environment.GetEnvironmentVariable("FORTRA_AUTH_TOKEN");
            emailFromAddress = Environment.GetEnvironmentVariable("EMAIL_FROM_ADDRESS");
            emailFromName = Environment.GetEnvironmentVariable("EMAIL_FROM_NAME");
            emailTo = Environment.GetEnvironmentVariable("EMAIL_TO");
            
            string? gmailerJson = Environment.GetEnvironmentVariable("GMAILER_GOOGLE_SERVICE_ACCOUNT_JSON");

            if (string.IsNullOrEmpty(fortraAccountId) ||
                string.IsNullOrEmpty(fortraAuthToken) ||
                string.IsNullOrEmpty(emailFromAddress) ||
                string.IsNullOrEmpty(emailFromName) ||
                string.IsNullOrEmpty(emailTo) ||
                string.IsNullOrEmpty(gmailerJson))
            {
                throw new InvalidOperationException($"{logHeadline} Missing required environment variables: FORTRA_ACCOUNT_ID, FORTRA_AUTH_TOKEN, EMAIL_FROM_ADDRESS, EMAIL_FFROM_NAME, EMAIL_TO, or GMAILER_GOOGLE_SERVICE_ACCOUNT_JSON");
            }
            
            // Deserialize the JSON string from the environment variable into a GmailerGoogleServiceAccount object
            try
            {
                gmailerGoogleServiceAccount = JsonConvert.DeserializeObject<GmailerGoogleServiceAccount>(gmailerJson);
                if (gmailerGoogleServiceAccount == null)
                {
                    throw new Exception("Deserialized GmailerGoogleServiceAccount is null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{logHeadline} Error deserializing GMAILER_GOOGLE_SERVICE_ACCOUNT_JSON: {ex.Message}", ex);
            }
        }

        // Return secrets as tuple 
        if (fortraAccountId == null || fortraAuthToken == null || emailFromAddress == null || emailFromName == null || emailTo == null || gmailerGoogleServiceAccount == null){
            throw new InvalidOperationException($"{logHeadline} One or more required secrets are missing.");
        }
        return (fortraAccountId, fortraAuthToken, emailFromAddress, emailFromName, emailTo, gmailerGoogleServiceAccount);
    }
}
