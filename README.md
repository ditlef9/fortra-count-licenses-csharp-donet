# Fortra Count Licenses C# dotnet

![Logo](docs/fortra-count-licenses-csharp-donet-logo.png)

 [![.NET Framework](https://img.shields.io/badge/.NET%20Framework-%3E%3D%209.0-red.svg)](#)

**📕General overview**<br>
Fortra Count Licenses is a tool developed to help organizations track and manage their license usage through the Fortra API. This application allows users to retrieve and analyze the license information associated with their accounts, providing insights such as active licenses, account metrics, and usage data. The goal of this project is to facilitate license management and improve operational efficiency by integrating with Fortra's API and offering a simple way to visualize and manage license data.

**𝄜 Excel Report**<br>
A report will be generated with data for each customer (using the 
[ClosedXML](https://github.com/ClosedXML/ClosedXML) library).
Example:

| ID  | Name                 | AccountStatus | AccountNumber | MaxAgents | AgentsUsed | ScanoptsAvMaxWindowSize | ScanoptsAvWindowSize | UsageSummary Agent Scanning Used | UsageSummary Agent Scanning Allowed | UsageSummary Vulnerability Management (Internal) Used | UsageSummary Vulnerability Management (Internal) Allowed |
|-----|----------------------|---------------|---------------|-----------|------------|-------------------------|----------------------|---------------------------------|-------------------------------------|----------------------------------------------------------|------------------------------------------------------------|
| 1   | TechSolutions Inc.    | Active        | 100987654     | 50        | 30         | 5000                    | 4500                 | 25                              | 50                                  | 10                                                       | 40                                                         |
| 2   | Global Enterprises    | Suspended     | 200876543     | 100       | 75         | 10000                   | 8500                 | 60                              | 100                                 | 35                                                       | 80                                                         |
| 3   | FutureTech Ltd.       | Pending       | 300765432     | 75        | 45         | 7500                    | 7000                 | 40                              | 75                                  | 20                                                       | 60                                                         |



**📥Email**<br>
The report will be emailed to a receiver. 


**Enviroments supported**<br>

The application can run on:
 * 💻 Locally on Windows, Mac and Linux
 * 🌎 Google Cloud Run Functions with Secret Manager and Scheduler
 * ☁️ Azure

---

## Index

[🏠 1 How to run locally](#%EF%B8%8F-3-how-i-created-the-app)<br>
[☁️ 2 How to deploy to Azure](#%EF%B8%8F-3-how-i-created-the-app)<br>
[🌎 3 How to deploy to Google Cloud](#%EF%B8%8F-3-how-i-created-the-app)<br>
[🛠️ 4 How I created the app](#%EF%B8%8F-3-how-i-created-the-app)<br>
[👨🏻‍🏫 5 Application presentation](#-4-application-presentation)<br>
[📜 6 License](#-5-license)<br>

---

## 🏠 1 How to run locally

### Prerequisites
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- API Key from [Fortra (https://vm.se.frontline.cloud/)](https://vm.se.frontline.cloud/)

### Steps

**1. Clone this repository:**
   ```bash
   git clone https://github.com/ditlef9/fortra-count-licenses-csharp-donet.git
   cd fortra-count-licenses-csharp-donet
   ```
**2. Install dependencies (if any):**
   ```bash
   dotnet restore
   ```

**3. Decide how to read secrets:**<br>

There are three methods of reading secrets. They are `Enviroment variables`, `Google Cloud Secret Manager` or
`Azure Key Vault`.

*3.a Enviroment variables (for testing purposes):*

Add ACCOUNT_ID and AUTH_TOKEN:

Linux:<br>
   ```csharp
   export FORTRA_ACCOUNT_ID="your_account_id"
   export FORTRA_AUTH_TOKEN="your_api_token"
   ```
   
Windows:<br>
   ```csharp
   $env:FORTRA_ACCOUNT_ID="your_account_id"
   $env:FORTRA_AUTH_TOKEN="your_api_token"
   ```


*3.b Google Cloud Secret Manager*

Add GOOGLE_CLOUD_PROJECT to enviroment variable

Windows:<br>
   ```csharp
   $env:GOOGLE_CLOUD_PROJECT="sindre-dev-439512"
   ```

Create a secret in Google Cloud > Secret Manager:

* Name: fortra-count-licenses
* Secret value:
```
{
  "fortra_account_id": "12345",
  "fortra_auth_token": "abcdefghiklmnopqrstuvwxyz",
  "emailFrom": "me@email.com",
  "email_to": "me@email.com,you@email.com",
  "gmailer_google_service_account": {
    "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
    "auth_uri": "https://accounts.google.com/o/oauth2/auth",
    "client_email": "your-service-account@your-project.iam.gserviceaccount.com",
    "client_id": "110012742384433576204",
    "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/your-service-account%your-project.iam.gserviceaccount.com",
    "private_key": "-----BEGIN PRIVATE KEY-----\nMIIEvgIB...dLMO3a\n-----END PRIVATE KEY-----\n",
    "private_key_id": "some-random-id",
    "project_id": "your-project",
    "token_uri": "https://oauth2.googleapis.com/token",
    "type": "service_account"
  }
}
```
* Locations: europe-north1
* Labels: 
   - app: fortra-count-licenses
   - responsible: sindre


*3.c Azure Key Vault*




**4. Run the application:**
   ```bash
   dotnet run
   ```


---

## ☁️ 2 How to deploy to Azure

### Steps to deploy to Azure App Service
1. Ensure you have the Azure CLI installed ([Download here](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)).
2. Login to Azure:
   ```bash
   az login
   ```
3. Create an Azure Web App:
   ```bash
   az webapp create --resource-group MyResourceGroup --plan MyPlan --name WeatherAppCSharp --runtime "DOTNETCORE:7.0"
   ```
4. Publish the app:
   ```bash
   dotnet publish -c Release -o ./publish
   az webapp deploy --resource-group MyResourceGroup --name WeatherAppCSharp --src-path ./publish
   ```
5. Visit your deployed application via the Azure-provided URL.

---


## 🌎 3 How to deploy to Google Cloud

To run locally with Google Cloud:
```
gcloud init
gcloud auth application-default login
```

--- 

## 🛠️ 4 How I created the app

New console app:
```
dotnet new console -n WeatherApp
```

Added `HttpClient` to call the OpenWeatherMap API, parsed JSON responses using `System.Text.Json`, and formatted the output to display weather details in the console.

---

## 👨🏻‍🏫 5 Application presentation



---

## 📜 6 License

This project is licensed under the
[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

```
Copyright 2024 github.com/ditlef9

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0
```