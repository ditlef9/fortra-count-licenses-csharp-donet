
// Services/Fortra/FortraAccountDataResponse/AgentLicense.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FortraAPICall.Services;

public class AgentLicense
{
    [JsonProperty("key")]
    public string? Key { get; set; }

    [JsonProperty("max_agents")]
    public int? MaxAgents { get; set; }

    [JsonProperty("agents_used")]
    public int AgentsUsed { get; set; }

    [JsonProperty("date_issued")]
    public string? DateIssued { get; set; }

}
