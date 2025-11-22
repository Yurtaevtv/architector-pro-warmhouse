FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./DeviceService/DeviceService.csproj", "."]
RUN dotnet restore "DeviceService.csproj"
COPY . .
RUN dotnet build "DeviceService.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeviceService.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeviceService.dll"]