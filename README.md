
![Fortra Count Licenses C# dotnet](docs/fortra-count-licenses-csharp-donet-logo.png)

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

[🏠 1 How to run locally](#-1-how-to-run-locally)<br>
[☁️ 2 How to deploy to Azure](#%EF%B8%8F-2-how-to-deploy-to-azure)<br>
[🌎 3 How to deploy to Google Cloud](#-3-how-to-deploy-to-google-cloud)<br>
[🛠️ 4 How I created the app](#%EF%B8%8F-4-how-i-created-the-app)<br>
[👨🏻‍🏫 5 Application presentation](#-5-application-presentation)<br>
[📜 6 License](#-6-license)<br>

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

Add enviroment variables:<br>

```
$env:FORTRA_ACCOUNT_ID="your_account_id"
$env:FORTRA_AUTH_TOKEN="your_api_token"
$env:EMAIL_FROM_ADDRESS="me@email.com",
$env:EMAIL_FROM_NAME="Fortra Counting Service",
$env:EMAIL_TO="me@email.com,you@email.com",
```
Linux should use the format `export FORTRA_ACCOUNT_ID="your_account_id"`.
   

*3.b Google Cloud Secret Manager*

Add GOOGLE_CLOUD_PROJECT_ID to enviroment variable<br>

```
$env:GOOGLE_CLOUD_PROJECT_ID="sindre-dev-439512"
```

Create a secret in Google Cloud > Secret Manager:

* Name: fortra-count-licenses
* Secret value:
```
{
  "fortra_account_id": "12345",
  "fortra_auth_token": "abcdefghiklmnopqrstuvwxyz",
  "email_from_address": "me@email.com",
  "email_from_name": "Fortra Count Licenses Service",
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

Run it locally with using Google Cloud Secret Manager requires login:
```
gcloud init
gcloud auth application-default login
```

*3.c Azure Key Vault*

Create a Key Vault in Azure:

`az keyvault create --name "fortraKeyVault" --resource-group "yourResourceGroup" --location "westeurope"`

Store secrets in the Key Vault:

```
az keyvault secret set --vault-name MyKeyVault --name "fortra_account_id" --value "12345"
az keyvault secret set --vault-name MyKeyVault --name "fortra_auth_token" --value "abcdefghiklmnopqrstuvwxyz"
az keyvault secret set --vault-name MyKeyVault --name "email_from_address" --value "me@email.com"
az keyvault secret set --vault-name MyKeyVault --name "email_from_name" --value "Fortra Count Licenses Service"
az keyvault secret set --vault-name MyKeyVault --name "email_to" --value "me@email.com,you@email.com"
```




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

**1 Create service account for sending emails**


**2 Create a secret `fortra-count-licenses`**:<br>
Google Cloud > Secret Manager > + New. See the format at *1 How to run locally -> 3.b Google Cloud Secret Manager*.


**3 Create a Docker repository in Artifact Registry and a Cloud Build bucket:**<br>

```commandline
gcloud artifacts repositories create docker-repo --repository-format=docker --location=europe-north1 --description="Docker repository" --project=sindre-dev-439512

gcloud artifacts repositories list --project=sindre-dev-439512

gcloud storage buckets create gs://sindre-dev-439512_cloudbuild --location=europe-north1 --project=sindre-dev-439512
```


**Create a Dockerfile**<br>
```
# Locally:
docker build -t fortra-count-licenses .

# To Google Cloud:
gcloud builds submit --region=europe-north1 --tag europe-north1-docker.pkg.dev/sindre-dev-439512/docker-repo/fortra-count-licenses .  --project=sindre-dev-439512

```

The Docker image should be available here:
https://console.cloud.google.com/artifacts/docker/sindre-dev-439512/europe-north1/docker-repo?project=sindre-dev-439512


**3 Deploy to Google Cloud Run:**<br>

Google Cloud > Cloud Run > [Create Service] 

* [v] Deploy one revision from an existing container image
* Container image URL: europe-north1-docker.pkg.dev/sindre-dev-439512/docker-repo/fortra-count-licenses@sha256:e3bd850f8059f473009b7b487b8f9315feffe6d0bf9e772288982c6ba2f79daa
* Region: europe-north1
* Authentication: Require autoentication
* Billing: Request based
* Ingress: All

Containers:
* Settings:
   * Revision scaling
      * Minimum number of instances: 0
      * Maximum number of instances: 1

* Variables & Secrets:
   * Enviroment variables:
      * GOOGLE_CLOUD_PROJECT_ID = sindre-dev-439512
   * Request timeout: 3600 (seconds)

```
# See your dotnet version
dotnet --version

# Login to GCP
gcloud auth login

# Deploy the application
gcloud functions deploy fortra-count-licenses --gen2 --runtime=dotnet8 --region=europe-north1 --source=. --entry-point=FortraCountLicenses.Program --trigger-http --timeout=540 --verbosity=info --project=sindre-dev-439512 --set-env-vars=GOOGLE_CLOUD_PROJECT_ID=sindre-dev-439512
```

Get the URL to the Google Cloud Run Function here: 
https://console.cloud.google.com/functions/details/europe-north1/fortra-count-licenses?project=sindre-dev-439512

**4 Add scheduler:**<br>
Google Cloud > Schedulerer > + New

Point it to the Google Cloud Run Function URL.

--- 

## 🛠️ 4 How I created the app

New console app:
```
dotnet new console -n FortraCountLicenses
```




---

## 👨🏻‍🏫 5 Application presentation


### 5.1 **Fortra Count Licenses – Automating License Tracking & Reporting**  

**Fortra Count Licenses** is a lightweight and efficient tool designed to help organizations **track, analyze, and manage** their license usage with Fortra’s API. This application automatically retrieves license data, generates an **Excel report**, and sends it via **email**—all in a fully automated workflow.  

With support for **Google Cloud Run, Azure**, and local execution, this tool integrates seamlessly into existing infrastructures. It provides visibility into **active licenses, usage data, and account statuses**, helping businesses optimize their license consumption.  

---

### 5.2 🚀 **Key Features**  

✅ **Automated API Integration** – Retrieves real-time license data from Fortra’s API.  
✅ **Excel Reporting** – Generates detailed reports using **ClosedXML** for easy analysis.  
✅ **Email Notifications** – Sends reports automatically to specified recipients.  
✅ **Secure Secret Management** – Uses **Google Cloud Secret Manager** or **Azure Key Vault** for credentials.  
✅ **Multi-Cloud Support** – Deployable on **Google Cloud Run Functions** and **Azure App Services**.  
✅ **Flexible Execution** – Run locally on **Windows, Mac, and Linux**.  
✅ **Scheduler Integration** – Automate execution using **Google Cloud Scheduler** or **Azure Functions Timer Triggers**.  

---

### 5.3 📊 **How It Works**  

1️⃣ **Retrieves Credentials & API Keys**  
   - Reads secrets from **Google Cloud Secret Manager**, **Azure Key Vault**, or **environment variables**.  

2️⃣ **Calls Fortra’s API**  
   - Fetches license data and account status information.  

3️⃣ **Processes & Formats Data**  
   - Structures the data into a clean, easy-to-read format.  

4️⃣ **Generates an Excel Report**  
   - Uses **ClosedXML** to create an Excel file with license details.  

5️⃣ **Sends Email with the Report**  
   - Uses **Gmail API** to deliver the report to configured recipients.  

---

### 5.4 🎯 **Use Cases**  

💼 **IT Asset Management** – Monitor software licenses and prevent overuse.  
📈 **Compliance & Auditing** – Ensure proper license tracking for audits.  
🔄 **Operational Efficiency** – Automate reporting and reduce manual tracking.  

Fortra Count Licenses simplifies license tracking, **reducing manual effort** and ensuring compliance across organizations. 🚀


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