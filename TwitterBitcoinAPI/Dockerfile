# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /TwitterBitcoinAPI

COPY TwitterBitcoinAPI/*.csproj .

RUN dotnet restore

COPY TwitterBitcoinAPI .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

COPY --from=build-env /publish .

EXPOSE 57693

ENTRYPOINT ["dotnet", "TwitterBitcoinAPI.dll", "--urls", "http://*:57693"]