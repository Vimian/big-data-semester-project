# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env

WORKDIR /GoldWebSocket

COPY GoldWebSocket/*.csproj .

RUN dotnet restore

COPY GoldWebSocket .

COPY GoldWebSocket/m5.csv ./m5.csv

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

COPY --from=build-env /publish .

EXPOSE 57695

ENTRYPOINT [ "dotnet", "GoldWebSocket.dll", "127.0.0.1", "57695", "m5.csv"]