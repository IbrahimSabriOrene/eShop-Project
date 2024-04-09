FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/LogControl/LogControl.csproj", "/src/LogControl/"]
COPY ["/Logger/Logger.csproj", "/src/Logger/"]

RUN dotnet restore "LogControl/LogControl.csproj"

COPY . .
WORKDIR /src/LogControl
RUN dotnet build "LogControl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogControl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogControl.dll"]