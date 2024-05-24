#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderingKioskSystemManagement/OrderingKioskSystemManagement.Api.csproj", "OrderingKioskSystemManagement/"]
COPY ["OrderingKioskSystem.Application/OrderingKioskSystem.Application.csproj", "OrderingKioskSystem.Application/"]
COPY ["OrderingKioskSystem.Domain/OrderingKioskSystem.Domain.csproj", "OrderingKioskSystem.Domain/"]
COPY ["OrderingKioskSystem.Infrastructure/OrderingKioskSystem.Infrastructure.csproj", "OrderingKioskSystem.Infrastructure/"]
RUN dotnet restore "./OrderingKioskSystemManagement/OrderingKioskSystemManagement.Api.csproj"
COPY . .
WORKDIR "/src/OrderingKioskSystemManagement"
RUN dotnet build "./OrderingKioskSystemManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderingKioskSystemManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderingKioskSystemManagement.Api.dll"]