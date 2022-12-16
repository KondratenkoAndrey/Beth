FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Identity/Beth.Identity.Api/Beth.Identity.Api.csproj", "Services/Identity/Beth.Identity.Api/"]
COPY ["Services/Identity/Beth.Identity.Application/Beth.Identity.Application.csproj", "Services/Identity/Beth.Identity.Application/"]
RUN dotnet restore "Services/Identity/Beth.Identity.Api/Beth.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Beth.Identity.Api"
RUN dotnet build "Beth.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Beth.Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Beth.Identity.Api.dll"]