
// Services/Fortra/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;


public class AccountThemeDetails
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("default")]
    public bool Default { get; set; }

    [JsonProperty("system_default")]
    public bool SystemDefault { get; set; }

    [JsonProperty("company_url")]
    public string? CompanyUrl { get; set; }

    [JsonProperty("company_name")]
    public string? CompanyName { get; set; }

    [JsonProperty("support_url")]
    public string? SupportUrl { get; set; }

    [JsonProperty("branded_title")]
    public string? BrandedTitle { get; set; }

    [JsonProperty("branded_title_short")]
    public string? BrandedTitleShort { get; set; }

    [JsonProperty("powered_by_url")]
    public string? PoweredByUrl { get; set; }

    [JsonProperty("powered_by_name")]
    public string? PoweredByName { get; set; }

    [JsonProperty("support_phone")]
    public string? SupportPhone { get; set; }

    [JsonProperty("email_support")]
    public string? EmailSupport { get; set; }

    [JsonProperty("virtual_hostname")]
    public string? VirtualHostname { get; set; }

    [JsonProperty("email_system")]
    public string? EmailSystem { get; set; }

    [JsonProperty("email_from")]
    public string? EmailFrom { get; set; }

    [JsonProperty("email_bcc")]
    public string? EmailBcc { get; set; }

    [JsonProperty("email_sales")]
    public string? EmailSales { get; set; }

    [JsonProperty("primary_color")]
    public string? PrimaryColor { get; set; }

    [JsonProperty("secondary_color")]
    public string? SecondaryColor { get; set; }

    [JsonProperty("text_color")]
    public string? TextColor { get; set; }

    [JsonProperty("report_watermark")]
    public string? ReportWatermark { get; set; }

    [JsonProperty("report_watermark_landscape")]
    public string? ReportWatermarkLandscape { get; set; }

    [JsonProperty("company_logo")]
    public string? CompanyLogo { get; set; }

    [JsonProperty("login_logo")]
    public string? LoginLogo { get; set; }

    [JsonProperty("favicon")]
    public string? Favicon { get; set; }

    [JsonProperty("security_seal_logo")]
    public string? SecuritySealLogo { get; set; }

    [JsonProperty("security_seal_alt_logo")]
    public string? SecuritySealAltLogo { get; set; }  // Nullable string

    [JsonProperty("account_name")]
    public string? AccountName { get; set; }

    [JsonProperty("available_regions")]
    public List<string> AvailableRegions { get; set; } = new List<string>();  // Initialized to avoid null
}