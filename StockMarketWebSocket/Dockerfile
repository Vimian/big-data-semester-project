# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /StockMarketWebSocket

COPY StockMarketWebSocket/*.csproj .

RUN dotnet restore

COPY StockMarketWebSocket .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

COPY --from=build-env /publish .

EXPOSE 57694

ENTRYPOINT [ "dotnet", "StockMarketWebSocket.dll", "127.0.0.1", "57694"]