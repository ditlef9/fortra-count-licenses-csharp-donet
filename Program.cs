// Program.cs

namespace FortraCountLinceses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FortraAPICall.Services;
class Program{
    static async Task Main()
    {
        Console.WriteLine("Program·Main·Starting API request...");

        // Call Fortra
        using var httpClient = new HttpClient();
        var fortraAPICall = new FortraAPICall(httpClient);
        FortraAccountDataResponse fortraAccountDataResponse = await fortraAPICall.CallAPI();
        foreach (var result in fortraAccountDataResponse.Results)
        {
            Console.WriteLine($"Account Name: {result.Name}");
            Console.WriteLine($"AccountStatus: {result.AccountStatus ?? "N/A"}");
            Console.WriteLine($"AccountType: {result.AccountType ?? "N/A"}");
        }

        Console.WriteLine("Program·Main·API request completed.");
    }
}