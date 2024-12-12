FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /main
COPY ["Region.Persistence.Api/Region.Persistence.Api.csproj", "Region.Persistence.Api/"]
RUN dotnet restore "Region.Persistence.Api/Region.Persistence.Api.csproj"
COPY . .

RUN dotnet build "Region.Persistence.Api/Region.Persistence.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Region.Persistence.Api/Region.Persistence.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Region.Persistence.Api.dll"]
