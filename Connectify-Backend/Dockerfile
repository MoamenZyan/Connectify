# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY Connectify.sln ./
COPY Connectify.API/Connectify.API.csproj Connectify.API/
COPY Connectify.Application/Connectify.Application.csproj Connectify.Application/
COPY Connectify.Domain/Connectify.Domain.csproj Connectify.Domain/
COPY Connectify.Infrastructure/Connectify.Infrastructure.csproj Connectify.Infrastructure/
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Connectify.API.dll"]
