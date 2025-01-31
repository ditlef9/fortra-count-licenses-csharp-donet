// Program.cs

namespace FortraCountLicenses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FortraAPICall.Services;
using FortraCountLicenses.Services.Excel;
using FortraCountLicenses.Utils.GCP;
using Newtonsoft.Json;

class Program{
    static async Task Main()
    {
        Console.WriteLine("Program·Main·Starting API request...");

        // How to access secrets, either via GCP Secret Manger (we need GOOGLE_CLOUD_PROJECT set)
        string howToAccessSecrets  = "";
        string? googleCloudProjectId = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT");
        if (!string.IsNullOrEmpty(googleCloudProjectId))
        {
            howToAccessSecrets = "Google Cloud Secret Manager";
        }
        if (howToAccessSecrets == ""){
            throw new Exception("Program·Main·howToAccessSecrets not found.");
        }




        // Access GCP Secret
        var secretReader = new GoogleSecretManagerReader();
        string mySecret = secretReader.ReadSecret("fortra-count-licenses");

        // Deserialize the JSON into SecretData class
        SecretData secretData = JsonConvert.DeserializeObject<SecretData>(mySecret);
        if (secretData == null)
        {
            throw new Exception("Error: Secret data could not be deserialized.");
        }

        Console.WriteLine($"Fortra Account ID: {secretData.FortraAccountId}");
        Console.WriteLine($"Google Service Account Email: {secretData.GmailerGoogleServiceAccount.ClientEmail}");


        // Call Fortra
        using var httpClient = new HttpClient();
        var fortraAPICall = new FortraAPICall(httpClient);
        FortraAccountDataResponse fortraAccountDataResponse = await fortraAPICall.CallAPI();
        foreach (var result in fortraAccountDataResponse.Results)
        {
            Console.WriteLine($"Program·Main·Found {result.Name} {result.AccountStatus ?? "N/A"} AccountType: {result.AccountType ?? "N/A"}");
        }

        Console.WriteLine("Program·Main·API request completed.");

        
        // Generate the Excel file
        var excelCreator = new ExcelFileCreator();
        excelCreator.CreateExcelFile(fortraAccountDataResponse.Results);

        Console.WriteLine("Excel file has been created successfully.");
        
    }
}