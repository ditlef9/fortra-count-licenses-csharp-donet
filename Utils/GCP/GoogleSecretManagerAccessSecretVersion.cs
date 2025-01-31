// Utils/Google/GoogleSecretManagerAccessSecretVersion.cs
namespace FortraCountLicenses.Utils.GCP;

using System;
using Google.Cloud.SecretManager.V1;

public class GoogleSecretManagerReader
{
    public string ReadSecret(string secretId, string versionId = "latest")
    {
        try
        {
            // Get the project ID from environment variable
            string? projectId = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT");
            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("Project ID not found. Set the 'GOOGLE_CLOUD_PROJECT' environment variable.");
            }

            // Create the client
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            // Build the secret version name
            string secretVersionName = $"projects/{projectId}/secrets/{secretId}/versions/{versionId}";

            // Access the secret version
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            // Decode the secret data
            return result.Payload.Data.ToStringUtf8();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error accessing secret: {ex.Message}");
            return string.Empty;
        }
    }
}