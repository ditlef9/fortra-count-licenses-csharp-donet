// Program.cs
namespace FortraCountLicenses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FortraAPICall.Services;
using FortraCountLicenses.Services.Excel;
using FortraCountLicenses.Utils.Email;
using FortraCountLicenses.Utils.Secrets; 

class Program
{
    static async Task Main()
    {
        string logHeadline = "Program·Main·";
        Console.WriteLine($"{logHeadline} Init...");

        // How to access secrets, either via GCP Secret Manager (we need GOOGLE_CLOUD_PROJECT set) ------------------
        Console.WriteLine($"{logHeadline} Howto access secret --------------------------------------");
        var (fortraAccountId, fortraAuthToken, emailFrom, emailTo, gmailerGoogleServiceAccountJson, howToAccessSecrets) = SecretManagerHelper.AccessSecrets();
        Console.WriteLine($"{logHeadline} fortraAccountId: {fortraAccountId}");


        // Email -----------------------------------------------------------------------------------------------------
        var gmailer = new Gmailer(gmailerGoogleServiceAccountJson, emailFrom);
        await gmailer.SendEmailAsync(
            emailTo: emailTo,
            emailSubject: "Hello!",
            emailContent: "<html><head></head><body><p>Hello, this is a test!</p></body></html>"
        );
        Console.WriteLine($"{logHeadline} Email sent.");

        // Call Fortra ----------------------------------------------------------------------------------------------
        Console.WriteLine($"{logHeadline} Call Fortra API --------------------------------------");
        if (fortraAccountId == null || fortraAuthToken == null)
        {
            throw new Exception($"{logHeadline} Missing credentials for Fortra API call.");
        }

        using var httpClient = new HttpClient();
        var fortraAPICall = new FortraAPICall(httpClient, fortraAccountId, fortraAuthToken);
        FortraAccountDataResponse fortraAccountDataResponse = await fortraAPICall.CallAPI();
        
        foreach (var result in fortraAccountDataResponse.Results)
        {
            Console.WriteLine($"{logHeadline} Found {result.Name} → {result.AccountStatus ?? "N/A"}");
        }

        Console.WriteLine($"{logHeadline} API request completed.");

        // Generate the Excel file ----------------------------------------------------------------------------------
        Console.WriteLine($"{logHeadline} Generate the Excel file -----------------------------");
        var excelCreator = new ExcelFileCreator();
        excelCreator.CreateExcelFile(fortraAccountDataResponse.Results);

        Console.WriteLine($"{logHeadline} Excel file has been created successfully.");

    }
}
