FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY WariSmart/*.csproj WariSmart/
RUN dotnet restore WariSmart/CatchUpPlatform.API.csproj
COPY . .
RUN dotnet publish WariSmart/CatchUpPlatform.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CatchUpPlatform.API.dll"]