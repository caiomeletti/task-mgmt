#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TM.API/TM.API.csproj", "TM.API/"]
COPY ["TM.Infrastructure/TM.Infrastructure.csproj", "TM.Infrastructure/"]
COPY ["TM.Domain/TM.Domain.csproj", "TM.Domain/"]
COPY ["TM.Core/TM.Core.csproj", "TM.Core/"]
COPY ["TM.Services/TM.Services.csproj", "TM.Services/"]
RUN dotnet restore "./TM.API/TM.API.csproj"
COPY . .
WORKDIR "/src/TM.API"
RUN dotnet build "./TM.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TM.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TM.API.dll"]