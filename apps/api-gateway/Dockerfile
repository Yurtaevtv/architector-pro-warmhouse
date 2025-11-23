FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./ApiGateway/ApiGateway.csproj", "."]
RUN dotnet restore "ApiGateway.csproj"
COPY . .
RUN dotnet build "ApiGateway.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]