// Services/Fortra/FortraAccountDataResponse/UsageSummary.cs

using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortraAPICall.Services
{
    public class UsageSummary
    {
        [JsonProperty("details")]
        public List<UsageDetail> Details { get; set; } = new List<UsageDetail>();

        [JsonProperty("has_overuse")]
        public bool HasOveruse { get; set; }

        [JsonProperty("max_percentage_overuse")]
        public double MaxPercentageOveruse { get; set; }
    }
}
