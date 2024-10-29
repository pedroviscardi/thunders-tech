# Use the official .NET 8 SDK as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
#EXPOSE 443

# Install OpenSSL if it's not already installed in the image
RUN apt-get update && apt-get install -y openssl

# Generate a self-signed certificate with OpenSSL
RUN mkdir /https && \
    openssl req -x509 -nodes -days 365 \
    -subj "/CN=localhost" \
    -addext "subjectAltName=DNS:localhost" \
    -newkey rsa:2048 -keyout /https/localhost.key -out /https/localhost.crt && \
    openssl pkcs12 -export -out /https/localhost.pfx -inkey /https/localhost.key -in /https/localhost.crt -password pass:yourpassword

# Set environment variables to configure HTTPS with the generated certificate
#ENV ASPNETCORE_URLS=https://+:443;http://+:5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=yourpassword

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

# Set entry point
ENTRYPOINT ["dotnet", "Thunders.Tecnologia.Api.dll"]