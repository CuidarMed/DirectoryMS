# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY DirectoryMS/DirectoryMS.csproj DirectoryMS/
RUN dotnet restore DirectoryMS/DirectoryMS.csproj
COPY . .
WORKDIR /src/DirectoryMS
RUN dotnet build DirectoryMS.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish DirectoryMS.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DirectoryMS.dll"]
