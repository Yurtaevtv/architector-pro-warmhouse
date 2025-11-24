FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root
WORKDIR /app
EXPOSE 5000

RUN apk update && \
    apk add curl && \
    rm -rf /var/cache/apk/*

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./WebSocketService/WebSocketService.csproj", "."]
RUN dotnet restore "WebSocketService.csproj"
COPY . .
RUN dotnet build "WebSocketService.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSocketService.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSocketService.dll"]