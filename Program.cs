// Program.cs


using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FortraAPICall.Services;
using FortraCountLicenses.Services.Excel;
using FortraCountLicenses.Utils.Email;
using FortraCountLicenses.Utils.Secrets; 
using DotNetEnv;

namespace HelloHttp;

public class Function : IHttpFunction
{
  private readonly ILogger _logger;

  public Function(ILogger<Function> logger) =>
    _logger = logger;

    public async Task HandleAsync(HttpContext context)
    {
        Env.Load();
        string logHeadline = "Program·Main·";
        Console.WriteLine($"{logHeadline} Init...");

        // How to access secrets, either via GCP Secret Manager (we need GOOGLE_CLOUD_PROJECT set) ------------------
        Console.WriteLine($"{logHeadline} Howto access secret --------------------------------------");
        var (fortraAccountId, fortraAuthToken, emailFromAddress, emailFromName, emailTo, gmailerGoogleServiceAccount) = SecretManagerHelper.AccessSecrets();
        Console.WriteLine($"{logHeadline} fortraAccountId: {fortraAccountId}");
        // Console.WriteLine($"{logHeadline} gmailerGoogleServiceAccount.ClientEmail: {gmailerGoogleServiceAccount.ClientEmail}");


        // Call Fortra ----------------------------------------------------------------------------------------------
        Console.WriteLine($"{logHeadline} Call Fortra API --------------------------------------");
        if (fortraAccountId == null || fortraAuthToken == null)
        {
            throw new Exception($"{logHeadline} Missing credentials for Fortra API call.");
        }

        using var httpClient = new HttpClient();
        var fortraAPICall = new FortraAPICall.Services.FortraAPICall(httpClient, fortraAccountId, fortraAuthToken);

        FortraAccountDataResponse fortraAccountDataResponse = await fortraAPICall.CallAPI();
        
        foreach (var result in fortraAccountDataResponse.Results)
        {
            Console.WriteLine($"{logHeadline} Found {result.Name} → {result.AccountStatus ?? "N/A"}");
        }

        Console.WriteLine($"{logHeadline} API request completed.");


        // Generate the Excel file ----------------------------------------------------------------------------------
        Console.WriteLine($"{logHeadline} Generate the Excel file -----------------------------");
        var excelCreator = new ExcelFileCreator();
        string excelFilePath = excelCreator.CreateExcelFile(fortraAccountDataResponse.Results);
        Console.WriteLine($"{logHeadline} Excel file has been created successfully at {excelFilePath}.");
        byte[] excelFileBytes = File.ReadAllBytes(excelFilePath);



        // Email -----------------------------------------------------------------------------------------------------
        
        // Prepare dynamic values for the email
        string currentMonthYear = DateTime.Now.ToString("MMMM yyyy");

        var gmailer = new Gmailer(gmailerGoogleServiceAccount, emailFromAddress);

        // Split multiple email recipients by comma
        string[] emailRecipients = emailTo.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var recipient in emailRecipients)
        {
            string trimmedEmail = recipient.Trim(); // Remove any extra spaces
            string recipientName = trimmedEmail.Split('@')[0]; // Extract name from email
            recipientName = char.ToUpper(recipientName[0]) + recipientName.Substring(1); // Capitalize first letter

            Console.WriteLine($"{logHeadline} Sending email to: {trimmedEmail} (Name: {recipientName})");

            await gmailer.SendEmailAsync(
                emailTo: trimmedEmail,
                emailSubject: $"Fortra Count Licenses for {DateTime.Now:MMMM yyyy} (sent {DateTime.Now:dd.MM.yy HH:mm})",
                emailContent: $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 20px auto;
                                background: #fff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                            }}
                            h1 {{
                                color: #333;
                                font-size: 22px;
                            }}
                            p {{
                                font-size: 16px;
                                color: #555;
                                line-height: 1.6;
                            }}
                            .footer {{
                                margin-top: 20px;
                                font-size: 12px;
                                color: #888;
                                text-align: center;
                            }}
                        </style>
                    </head>
                    <body>
                        <div style='height:10px;'></div>
                        <div class='container'>
                            <h1>Hello {recipientName},</h1>
                            <p>Your monthly Fortra Count Licenses report is available as an Excel sheet attached to this email.</p>
                            <p>Best regards,</p>
                            <p><strong>{emailFromName}</strong></p>
                            <hr>
                            <p class='footer'>If you wish to unsubscribe simply reply to this email or use the 
                            <a href='mailto:{emailFromAddress}?subject=Unsubscribe'>Unsubscribe</a> link.</p>
                        </div>
                        <div style='height:10px;'></div>
                    </body>
                    </html>",
                attachmentFileName: $"Fortra-Count-Licenses-{DateTime.Now:yyy-MM}.xlsx",
                attachmentBytes: excelFileBytes,
                attachmentMimeType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            );
            
            Console.WriteLine($"{logHeadline} Email sent to {trimmedEmail}.");
        } // foreach email receipient


        // Return
        await context.Response.WriteAsync($"Hello !");
    }
}
