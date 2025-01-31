
// Services/Fortra/FortraAccountDataResponse.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;


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