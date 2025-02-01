FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
COPY --from=build /App/out .

# Set the environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

# Expose port 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FortraCountLicenses.dll"]
