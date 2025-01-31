// Utils\Google\SecretData.cs
namespace FortraCountLicenses.Utils.GCP;

public class GmailerGoogleServiceAccount
{
    public string AuthProviderX509CertUrl { get; set; }
    public string AuthUri { get; set; }
    public string ClientEmail { get; set; }
    public string ClientId { get; set; }
    public string ClientX509CertUrl { get; set; }
    public string PrivateKey { get; set; }
    public string PrivateKeyId { get; set; }
    public string ProjectId { get; set; }
    public string TokenUri { get; set; }
    public string Type { get; set; }
}

public class SecretData
{
    public string FortraAccountId { get; set; }
    public string FortraAuthToken { get; set; }
    public GmailerGoogleServiceAccount GmailerGoogleServiceAccount { get; set; }
}
