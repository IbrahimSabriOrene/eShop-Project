
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /srcb
COPY . .
RUN dotnet restore "Basket.Api.csproj"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish 


FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Basket.Api.dll"]
