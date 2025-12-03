# Build stage
ENTRYPOINT ["dotnet", "neurozen.API.dll"]

ENV ASPNETCORE_URLS=http://+:8080
# Set environment variable for ASP.NET Core

EXPOSE 8080
# Expose port

COPY --from=build /app/publish .
WORKDIR /app
FROM mcr.microsoft.com/dotnet/aspnet:9.0
# Runtime stage

RUN dotnet publish -c Release -o /app/publish
WORKDIR /source/neurozen.API
COPY neurozen.API/. ./neurozen.API/
# Copy everything else and build

RUN dotnet restore
COPY neurozen.API/*.csproj ./neurozen.API/
COPY neurozen.API.sln .
# Copy csproj and restore dependencies

WORKDIR /source
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

