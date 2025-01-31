// Services/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services
{
    public class FortraAccountDataResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string? Next { get; set; }  // Can be a string or null

        [JsonProperty("previous")]
        public string? Previous { get; set; }  // Can be a string or null

        [JsonProperty("results")]
        public List<Result> Results { get; set; } = new List<Result>();  // Initialized to avoid null
    }

    public class Result
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_stats")]
        public UserStats? UserStats { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("account_status")]
        public string? AccountStatus { get; set; }

        [JsonProperty("account_type")]
        public string? AccountType { get; set; }

        [JsonProperty("account_class")]
        public string? AccountClass { get; set; }

        [JsonProperty("account_metrics")]
        public AccountMetrics? AccountMetrics { get; set; }

        [JsonProperty("vertical")]
        public string? Vertical { get; set; }

        [JsonProperty("total_employees")]
        public string? TotalEmployees { get; set; }

        [JsonProperty("annual_revenue")]
        public string? AnnualRevenue { get; set; }

        [JsonProperty("ownee_access_level")]
        public string? OwneeAccessLevel { get; set; }

        [JsonProperty("password_expiration_enabled")]
        public bool PasswordExpirationEnabled { get; set; }

        [JsonProperty("restricted_access")]
        public bool RestrictedAccess { get; set; }

        [JsonProperty("fortra_d365_crm_id")]
        public string? FortraD365CrmId { get; set; }  // Nullable string

        [JsonProperty("account_external_crm_id")]
        public string? AccountExternalCrmId { get; set; }  // Nullable string

        [JsonProperty("password_expiration_days")]
        public int PasswordExpirationDays { get; set; }

        [JsonProperty("require_email_tls")]
        public bool RequireEmailTls { get; set; }

        [JsonProperty("require_passworded_reports")]
        public bool RequirePasswordedReports { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("account_number")]
        public string? AccountNumber { get; set; }

        [JsonProperty("account_external_id")]
        public string? AccountExternalId { get; set; }  // Nullable string

        [JsonProperty("language_code")]
        public string? LanguageCode { get; set; }

        [JsonProperty("account_advocate_details")]
        public AccountAdvocateDetails? AccountAdvocateDetails { get; set; }

        [JsonProperty("account_manager_details")]
        public string? AccountManagerDetails { get; set; }  // Nullable string

        [JsonProperty("account_theme_details")]
        public AccountThemeDetails? AccountThemeDetails { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("date_last_login")]
        public DateTime? DateLastLogin { get; set; }  // Nullable DateTime
    }

    public class UserStats
    {
        [JsonProperty("has_locked_user")]
        public bool HasLockedUser { get; set; }

        [JsonProperty("using_api_tokens")]
        public bool UsingApiTokens { get; set; }

        [JsonProperty("using_adfs")]
        public bool UsingAdfs { get; set; }

        [JsonProperty("email_login_disabled")]
        public bool EmailLoginDisabled { get; set; }

        [JsonProperty("using_duosec")]
        public bool UsingDuosec { get; set; }

        [JsonProperty("interactive_access_disabled")]
        public bool InteractiveAccessDisabled { get; set; }
    }

    public class AccountMetrics
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_name")]
        public string? AccountName { get; set; }

        [JsonProperty("account")]
        public int Account { get; set; }

        [JsonProperty("vertical")]
        public int? Vertical { get; set; }

        [JsonProperty("total_employees")]
        public int? TotalEmployees { get; set; }  // Change to nullable int

        [JsonProperty("annual_revenue")]
        public int? AnnualRevenue { get; set; }
    }

    public class AccountAdvocateDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fullname")]
        public string? Fullname { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }
    }

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
}
