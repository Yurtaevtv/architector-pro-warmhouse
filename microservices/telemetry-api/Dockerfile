FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./Telemetry.Api/Telemetry.Api.csproj", "."]
RUN dotnet restore "Telemetry.Api.csproj"
COPY . .
RUN dotnet build "Telemetry.Api.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telemetry.Api.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telemetry.Api.dll"]