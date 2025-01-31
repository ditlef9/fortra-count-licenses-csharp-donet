// Services/Fortra/FortraAccountDataResponse/UsageDetail.cs

using Newtonsoft.Json;

namespace FortraAPICall.Services
{
    public class UsageDetail
    {
        [JsonProperty("used")]
        public int Used { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; } = string.Empty;

        [JsonProperty("total_allowed")]
        public int TotalAllowed { get; set; }

        [JsonProperty("usage_equation")]
        public string UsageEquation { get; set; } = string.Empty;

        [JsonProperty("billing_context")]
        public string BillingContext { get; set; } = string.Empty;

        [JsonProperty("percentage_used")]
        public double PercentageUsed { get; set; }

        [JsonProperty("service_offering")]
        public string ServiceOffering { get; set; } = string.Empty;
    }
}
