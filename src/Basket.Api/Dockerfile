FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Basket.Api/Basket.Api.csproj", "Basket.Api/"]

RUN dotnet restore "Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
