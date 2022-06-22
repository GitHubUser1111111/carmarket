#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#COPY ["Carmarket.Application.Proxy/ssl/carmarket.users.pfx", "/etc/ssl/certs/carmarket.users.pfx"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.users.crt", "/etc/ssl/certs/carmarket.users.crt"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.users.key", "/etc/ssl/certs/carmarket.users.key"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.crt", "/usr/local/share/ca-certificates/carmarket.identity.crt"]
#COPY ["Carmarket.Application.Proxy/ssl/carmarket.identity.crt", "/etc/ssl/certs/carmarket.identity.crt"]
#
#RUN ["update-ca-certificates"]

WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Carmarket.Microservice.Users/Carmarket.Microservice.Users.csproj", "Carmarket.Microservice.Users/"]
COPY ["Carmarket.Application.Identity.IS4/Carmarket.Application.Identity.IS4.csproj", "Carmarket.Application.Identity.IS4/"]
COPY ["Carmarket.Domain.Identity/Carmarket.Domain.Identity.csproj", "Carmarket.Domain.Identity/"]
COPY ["Carmarket.Infrastructure/Carmarket.Infrastructure.csproj", "Carmarket.Infrastructure/"]
COPY ["Carmarket.Infrastructure.Data.UsersDbContext/Carmarket.Infrastructure.Data.UsersDbContext.csproj", "Carmarket.Infrastructure.Data.UsersDbContext/"]
RUN dotnet restore "Carmarket.Microservice.Users/Carmarket.Microservice.Users.csproj"
COPY . .

WORKDIR "/src/Carmarket.Microservice.Users"
RUN dotnet build "Carmarket.Microservice.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carmarket.Microservice.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Carmarket.Microservice.Users.dll"]