
// Services/Fortra/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;

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
