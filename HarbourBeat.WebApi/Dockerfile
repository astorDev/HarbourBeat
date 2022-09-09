FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HarbourBeat.WebApi/HarbourBeat.WebApi.csproj", "HarbourBeat.WebApi/"]
COPY ["HarbourBeat.Protocol/HarbourBeat.Protocol.csproj", "HarbourBeat.Protocol/"]
RUN dotnet restore "HarbourBeat.WebApi/HarbourBeat.WebApi.csproj"
COPY . .
WORKDIR "/src/HarbourBeat.WebApi"
RUN dotnet build "HarbourBeat.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HarbourBeat.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG KIBANA_URL=http://localhost:5601
ENTRYPOINT dotnet HarbourBeat.WebApi.dll --KibanaUrl=$KIBANA_URL