# Build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV ASPNETCORE_ENVIRONMENT=Production

WORKDIR /source

COPY . .

RUN dotnet restore "./src/Web/Web.csproj" --disable-parallel

RUN dotnet publish "./src/Web/Web.csproj" -o /app --no-restore

# Serve

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]
