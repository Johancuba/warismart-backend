FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /app

# Copy csproj and restore dependencies
COPY WariSmart/*.csproj WariSmart/
RUN dotnet restore WariSmart/WariSmart.API.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish WariSmart/WariSmart.API.csproj -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=builder /app/out .

# Railway injects PORT environment variable automatically
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE ${PORT:-8080}

ENTRYPOINT ["dotnet", "WariSmart.API.dll"]