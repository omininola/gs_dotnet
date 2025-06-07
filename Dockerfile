﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN useradd -m tavin

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NaturalDisasterAPI/NaturalDisasterAPI.csproj", "NaturalDisasterAPI/"]
RUN dotnet restore "NaturalDisasterAPI/NaturalDisasterAPI.csproj"
COPY . .
WORKDIR "/src/NaturalDisasterAPI"
RUN dotnet build "./NaturalDisasterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NaturalDisasterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER tavin
ENTRYPOINT ["dotnet", "NaturalDisasterAPI.dll"]
