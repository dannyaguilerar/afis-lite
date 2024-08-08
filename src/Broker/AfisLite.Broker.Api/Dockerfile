#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Broker/AfisLite.Broker.Api/AfisLite.Broker.Api.csproj", "Broker/AfisLite.Broker.Api/"]
COPY ["Broker/AfisLite.Broker.Core/AfisLite.Broker.Core.csproj", "Broker/AfisLite.Broker.Core/"]
COPY ["Broker/AfisLite.Broker.Infra/AfisLite.Broker.Infra.csproj", "Broker/AfisLite.Broker.Infra/"]
RUN dotnet restore "./Broker/AfisLite.Broker.Api/AfisLite.Broker.Api.csproj"
COPY . .
WORKDIR "/src/Broker/AfisLite.Broker.Api"
RUN dotnet build "./AfisLite.Broker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AfisLite.Broker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AfisLite.Broker.Api.dll"]