# Fortra Count Licenses C# dotnet

![Logo](docs/fortra-count-licenses-csharp-donet-logo.png)

Fortra Count Licenses is a tool developed to help organizations track and manage their license usage through the Fortra API. This application allows users to retrieve and analyze the license information associated with their accounts, providing insights such as active licenses, account metrics, and usage data. The goal of this project is to facilitate license management and improve operational efficiency by integrating with Fortra's API and offering a simple way to visualize and manage license data.

With an easy-to-use interface, the application allows users to fetch important data related to accounts and license counts, making it a valuable tool for system administrators, IT teams, and organizations looking to ensure compliance and optimize their licensing strategies.

In this guide, you will learn how to run the application locally, deploy it to Azure, and understand the key components of the codebase.

---

## Index

[🏠 1 How to run locally](#%EF%B8%8F-3-how-i-created-the-app)<br>
[☁️ 2 How to deploy to Azure](#%EF%B8%8F-3-how-i-created-the-app)<br>
[🛠️ 3 How I created the app](#%EF%B8%8F-3-how-i-created-the-app)<br>
[👨🏻‍🏫 4 Application presentation](#-4-application-presentation)<br>
[📜 5 License](#-5-license)<br>

---

## 🏠 1 How to run locally

### Prerequisites
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- API Key from [Fortra (https://vm.se.frontline.cloud/)](https://vm.se.frontline.cloud/)

### Steps
1. Clone this repository:
   ```bash
   git clone https://github.com/ditlef9/fortra-count-licenses-csharp-donet.git
   cd fortra-count-licenses-csharp-donet
   ```
2. Install dependencies (if any):
   ```bash
   dotnet restore
   ```
3. Add ACCOUNT_ID and AUTH_TOKEN:

    Linux:<br>
   ```csharp
   export ACCOUNT_ID="your_account_id"
   export AUTH_TOKEN="your_api_token"
   ```
   
    Windows:<br>
   ```csharp
   $env:ACCOUNT_ID="your_account_id"
   $env:AUTH_TOKEN="your_api_token"
   ```
4. Run the application:
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

## 🛠️ 3 How I created the app

New console app:
```
dotnet new console -n WeatherApp
```

Added `HttpClient` to call the OpenWeatherMap API, parsed JSON responses using `System.Text.Json`, and formatted the output to display weather details in the console.

---

## 👨🏻‍🏫 4 Application presentation



---

## 📜 5 License

This project is licensed under the
[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

```
Copyright 2024 github.com/ditlef9

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0
```