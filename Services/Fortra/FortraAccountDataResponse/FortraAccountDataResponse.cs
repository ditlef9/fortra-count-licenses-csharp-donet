
// Services/Fortra/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;
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
