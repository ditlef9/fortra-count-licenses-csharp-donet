// Services/Fortra/FortraAccountDataResponse/Result.cs

using Newtonsoft.Json;
using System;

namespace FortraAPICall.Services
{
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

        [JsonProperty("usage_summary")]
        public UsageSummary? UsageSummary { get; set; }  // New property added

        [JsonProperty("max_agents")]
        public int? MaxAgents { get; set; }

        [JsonProperty("agent_license")]
        public AgentLicense? AgentLicense { get; set; }

        [JsonProperty("scanopts_av_max_window_size")]
        public int? ScanoptsAvMaxWindowSize { get; set; }

        [JsonProperty("scanopts_av_window_size")]
        public int? ScanoptsAvWindowSize { get; set; }
    }
}
