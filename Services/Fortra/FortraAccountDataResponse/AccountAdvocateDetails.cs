
// Services/Fortra/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;

public class AccountAdvocateDetails
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("fullname")]
    public string? Fullname { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }
}