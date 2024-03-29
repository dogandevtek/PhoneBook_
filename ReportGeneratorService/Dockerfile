#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReportGeneratorService/ReportGeneratorService.csproj", "ReportGeneratorService/"]
COPY ["EventBus.Factory/EventBus.Factory.csproj", "EventBus.Factory/"]
COPY ["EventBus.Base/EventBus.Base.csproj", "EventBus.Base/"]
COPY ["EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "EventBus.RabbitMQ/"]
RUN dotnet restore "./ReportGeneratorService/./ReportGeneratorService.csproj"
COPY . .
WORKDIR "/src/ReportGeneratorService"
RUN dotnet build "./ReportGeneratorService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReportGeneratorService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportGeneratorService.dll"]



#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /app
#
#COPY ./bin/Release/net8.0/publish .
#ENTRYPOINT ["dotnet", "ReportGeneratorService.dll"]