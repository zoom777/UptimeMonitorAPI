FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY UptimeMonitor.API/*.csproj ./UptimeMonitor.API/
RUN dotnet restore ./UptimeMonitor.API/UptimeMonitor.API.csproj

COPY UptimeMonitor.API ./UptimeMonitor.API/
WORKDIR /app/UptimeMonitor.API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://0.0.0.0:8080/
EXPOSE 8080
ENTRYPOINT ["dotnet", "UptimeMonitor.API.dll"]