#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.pfx", "/etc/ssl/certs/carmarket.identity.pfx"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.crt", "/etc/ssl/certs/carmarket.identity.crt"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.key", "/etc/ssl/private/carmarket.identity.key"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.crt", "/usr/local/share/ca-certificates/carmarket.identity.crt"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.crt", "/etc/ssl/certs/carmarket.identity.crt"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.users.crt", "/usr/local/share/ca-certificates/carmarket.users.crt"]
#
#RUN ["update-ca-certificates"]

WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Carmarket.Application.Identity.IS4/Carmarket.Application.Identity.IS4.csproj", "Carmarket.Application.Identity.IS4/"]
COPY ["Carmarket.Domain.Identity/Carmarket.Domain.Identity.csproj", "Carmarket.Domain.Identity/"]
RUN dotnet restore "Carmarket.Application.Identity.IS4/Carmarket.Application.Identity.IS4.csproj"
COPY . .

WORKDIR "/src/Carmarket.Application.Identity.IS4"
RUN dotnet build "Carmarket.Application.Identity.IS4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carmarket.Application.Identity.IS4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Carmarket.Application.Identity.IS4.dll"]