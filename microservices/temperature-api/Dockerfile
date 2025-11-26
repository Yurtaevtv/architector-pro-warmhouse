FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./Temperature.Api/Temperature.Api.csproj", "."]
RUN dotnet restore "Temperature.Api.csproj"
COPY . .
RUN dotnet build "Temperature.Api.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Temperature.Api.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Temperature.Api.dll"]