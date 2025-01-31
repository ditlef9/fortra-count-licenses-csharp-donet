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

    [JsonProperty("email_from")]
    public string? EmailFrom { get; set; }

    [JsonProperty("email_to")]
    public string? EmailTo { get; set; }

    [JsonProperty("gmailer_google_service_account")]
    public string? GmailerGoogleServiceAccount { get; set; }

}
