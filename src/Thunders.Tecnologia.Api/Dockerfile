# Use the official .NET 8 SDK as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj and restore as distinct layers
COPY ["Thunders.Tecnologia.Api/Thunders.Tecnologia.Api.csproj", "Thunders.Tecnologia.Api/"]
COPY ["Thunders.Tecnologia.Application/Thunders.Tecnologia.Application.csproj", "Thunders.Tecnologia.Application/"]
COPY ["Thunders.Tecnologia.Domain/Thunders.Tecnologia.Domain.csproj", "Thunders.Tecnologia.Domain/"]
COPY ["Thunders.Tecnologia.Infrastructure/Thunders.Tecnologia.Infrastructure.csproj", "Thunders.Tecnologia.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Thunders.Tecnologia.Api/Thunders.Tecnologia.Api.csproj"

# Copy all files and build the app
COPY . .
WORKDIR "/src/Thunders.Tecnologia.Api"
RUN dotnet build "Thunders.Tecnologia.Api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Thunders.Tecnologia.Api.csproj" -c Release -o /app/publish

# Final stage: runtime environment
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thunders.Tecnologia.Api.dll"]