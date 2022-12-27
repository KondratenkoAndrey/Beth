FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Beth.Catalog.Api/Beth.Catalog.Api.csproj", "Services/Catalog/Beth.Catalog.Api/"]
RUN dotnet restore "Services/Catalog/Beth.Catalog.Api/Beth.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Beth.Catalog.Api"
RUN dotnet build "Beth.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Beth.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Beth.Catalog.Api.dll"]
