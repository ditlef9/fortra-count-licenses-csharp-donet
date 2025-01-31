# Fortra Count Licenses

Introduction..

---

## Index

[🏠 1 How to run locally](#1-how-to-run-locally)<br>
[☁️ 2 How to deploy to Azure](#2-how-to-deploy-to-azure)<br>
[🛠️ 3 How I created the app](#3-how-i-created-the-app)<br>
[👨🏻‍🏫 4 Application presentation](#4-Application presentation)<br>
[📜 5 License](#5-license)<br>

---

## 🏠 1 How to run locally

### Prerequisites
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- API Key from [OpenWeatherMap](https://openweathermap.org/api)

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

## 📜 4 License

This project is licensed under the
[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

```
Copyright 2024 github.com/ditlef9

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0
```