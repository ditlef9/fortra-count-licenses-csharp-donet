// Utils\Google\SecretData.cs
using Newtonsoft.Json;

namespace FortraCountLicenses.Utils.GCP;
using Newtonsoft.Json;

public class SecretData
{
    [JsonProperty("fortra_account_id")]
    public string? FortraAccountId { get; set; }

    [JsonProperty("fortra_auth_token")]
    public string? FortraAuthToken { get; set; }

    [JsonProperty("gmailer_google_service_account")]
    public GmailerGoogleServiceAccount? GmailerGoogleServiceAccount { get; set; }
}

public class GmailerGoogleServiceAccount
{
    [JsonProperty("auth_provider_x509_cert_url")]
    public string? AuthProviderX509CertUrl { get; set; }

    [JsonProperty("auth_uri")]
    public string? AuthUri { get; set; }

    [JsonProperty("client_email")]
    public string? ClientEmail { get; set; }

    [JsonProperty("client_id")]
    public string? ClientId { get; set; }

    [JsonProperty("client_x509_cert_url")]
    public string? ClientX509CertUrl { get; set; }

    [JsonProperty("private_key")]
    public string? PrivateKey { get; set; }

    [JsonProperty("private_key_id")]
    public string? PrivateKeyId { get; set; }

    [JsonProperty("project_id")]
    public string? ProjectId { get; set; }

    [JsonProperty("token_uri")]
    public string? TokenUri { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }
}
