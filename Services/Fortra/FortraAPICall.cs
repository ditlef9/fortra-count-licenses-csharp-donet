// Services/Fortra/FortraAPICall.cs


namespace FortraAPICall.Services;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;  // For System.Text.Json serialization

public class FortraAPICall {
    private readonly HttpClient _httpClient;

    public FortraAPICall(HttpClient httpClient){
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<FortraAccountDataResponse> CallAPI(){
        string? accountId = Environment.GetEnvironmentVariable("ACCOUNT_ID");
        string? authToken = Environment.GetEnvironmentVariable("AUTH_TOKEN");

        if (string.IsNullOrEmpty(accountId)) {
            throw new InvalidOperationException("FortraAPICall·CallAPI·Missing Env var ACCOUNT_ID");
        }
        if (string.IsNullOrEmpty(authToken)) {
            throw new InvalidOperationException("FortraAPICall·CallAPI·Missing Env var AUTH_TOKEN");
        }

        string apiURL = $"https://vm.se.frontline.cloud/api/account/?account_id={accountId}";

        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiURL);
        request.Headers.Add("Authorization", $"Token {authToken}");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string jsonResponse = await response.Content.ReadAsStringAsync();

        // Save JSON (Pretty) using System.Text.Json
        string directory = "../../_tmp";
        string filePath = Path.Combine(directory, "FortraAPICall.json");
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }

        // Pretty print with System.Text.Json
        var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        string prettyJson = System.Text.Json.JsonSerializer.Serialize(jsonElement, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        
        await File.WriteAllTextAsync(filePath, prettyJson);

        // Deserialize with Newtonsoft.Json (or use System.Text.Json if preferred)
        FortraAccountDataResponse dataResponse = JsonConvert.DeserializeObject<FortraAccountDataResponse>(jsonResponse) ?? new FortraAccountDataResponse();

        return dataResponse;
    }
}
