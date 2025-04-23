FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY WarehouseOptimizer.sln ./
COPY src/ ./src/
RUN dotnet restore WarehouseOptimizer.sln

WORKDIR /app/src
RUN dotnet publish WarehouseOptimizer.Worker/WarehouseOptimizer.Worker.csproj -c $BUILD_CONFIGURATION -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WarehouseOptimizer.Worker.dll"]