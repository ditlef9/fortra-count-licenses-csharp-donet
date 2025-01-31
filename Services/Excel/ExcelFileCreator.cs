// Services/Excel/ExcelFileCreator.cs

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using FortraAPICall.Services;

namespace FortraCountLicenses.Services.Excel;

public class ExcelFileCreator
{
    public void CreateExcelFile(List<Result> results)
    {
        var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Account Data");

        // Define headers
        var headers = new string[]
        {
            "ID", "AccountNumber", "Name", "AccountStatus", 
            "MaxAgents", "AgentsUsed", "ScanoptsAvMaxWindowSize", "ScanoptsAvWindowSize",
            "Agent Scanning Used", "Agent Scanning Allowed",
            "Vulnerability Management (Internal) Used",
            "Vulnerability Management (Internal) Allowed"
        };

        // Add headers to the first row
        for (int col = 0; col < headers.Length; col++)
        {
            worksheet.Cell(1, col + 1).Value = headers[col];
        }

        // Apply filter to the header row
        worksheet.Range(1, 1, 1, headers.Length).SetAutoFilter();

        // Write data rows
        int rowIndex = 2;
        foreach (var row in results)
        {
            var agentScanning = row.UsageSummary?.Details?.FirstOrDefault(d => d.ServiceOffering == "agent_scanning");
            var vulnerabilityManagement = row.UsageSummary?.Details?.FirstOrDefault(d => d.ServiceOffering == "va");

            worksheet.Cell(rowIndex, 1).Value = row.Id;
            worksheet.Cell(rowIndex, 2).Value = row.AccountNumber ?? "N/A";
            worksheet.Cell(rowIndex, 3).Value = row.Name ?? "N/A";
            worksheet.Cell(rowIndex, 4).Value = row.AccountStatus ?? "N/A";
            worksheet.Cell(rowIndex, 5).Value = row.AgentLicense?.MaxAgents ?? 0;
            worksheet.Cell(rowIndex, 6).Value = row.AgentLicense?.AgentsUsed ?? 0;
            worksheet.Cell(rowIndex, 7).Value = row.ScanoptsAvMaxWindowSize ?? 0;
            worksheet.Cell(rowIndex, 8).Value = row.ScanoptsAvWindowSize ?? 0;
            worksheet.Cell(rowIndex, 9).Value = agentScanning?.Used ?? 0;
            worksheet.Cell(rowIndex, 10).Value = agentScanning?.TotalAllowed ?? 0;
            worksheet.Cell(rowIndex, 11).Value = vulnerabilityManagement?.Used ?? 0;
            worksheet.Cell(rowIndex, 12).Value = vulnerabilityManagement?.TotalAllowed ?? 0;

            rowIndex++;
        }

        // Auto-fit columns based on content (ensures the column is as wide as the header)
        worksheet.Columns().AdjustToContents();

        // Save the Excel file
        var filePath = "_tmp/FortraAccountData.xlsx";
        workbook.SaveAs(filePath);
        Console.WriteLine($"Excel file saved as '{filePath}'");
    }
}
