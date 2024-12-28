FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Kopiere die .csproj-Datei
COPY tschiraplusAPI/tschiraplusAPI.csproj .

# Kopiere den gesamten Code
COPY tschiraplusAPI/ .

# Restore die Abhängigkeiten
RUN dotnet restore

# Veröffentliche die Anwendung
RUN dotnet publish -c Release -o /app/publish

# Verwende das .NET Runtime-Image für die Produktion
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

# Kopiere die veröffentlichte Anwendung aus dem Build-Container
COPY --from=build /app/publish .

# Setze den Entry-Point (Startpunkt) der Anwendung
ENTRYPOINT ["dotnet", "tschiraplusAPI.dll"]
