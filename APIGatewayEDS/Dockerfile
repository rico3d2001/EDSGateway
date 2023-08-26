FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ValidacaoHelper/ValidacaoHelper.csproj", "ValidacaoHelper/"]
COPY ["EDSCore/EDSCore.csproj", "EDSCore/"]
COPY ["ContratoDTOs/ContratoDTOs.csproj", "ContratoDTOs/"]
COPY ["HubDTOs/HubDTOs.csproj", "HubDTOs/"]
COPY ["Hub.Dominio/Hub.Dominio.csproj", "Hub.Dominio/"]
COPY ["ServicoAutorizacao/ServicoAutorizacao.csproj", "ServicoAutorizacao/"]
COPY ["ServiceFotoUsuario/ServiceFotoUsuario.csproj", "ServiceFotoUsuario/"]
COPY ["APIGatewayEDS/APIGatewayEDS.csproj", "APIGatewayEDS/"]
COPY ["OrganizacaoDTOS/OrganizacaoDTOS.csproj", "OrganizacaoDTOS/"]
COPY ["ServicosEmailHub/ServicosEmailHub.csproj", "ServicosEmailHub/"]
RUN dotnet restore "APIGatewayEDS/APIGatewayEDS.csproj"
COPY . .
WORKDIR "/src/APIGatewayEDS"
RUN dotnet build "APIGatewayEDS.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "APIGatewayEDS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGatewayEDS.dll"]
