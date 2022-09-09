FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HarbourBeat.Background.Service/HarbourBeat.Background.Service.csproj", "HarbourBeat.Background.Service/"]
COPY ["HarbourBeat.Protocol/HarbourBeat.Protocol.csproj", "HarbourBeat.Protocol/"]
RUN dotnet restore "HarbourBeat.Background.Service/HarbourBeat.Background.Service.csproj"
COPY . .
WORKDIR "/src/HarbourBeat.Background.Service"
RUN dotnet build "HarbourBeat.Background.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HarbourBeat.Background.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG HARBOURBEAT_API_URL
ENTRYPOINT dotnet HarbourBeat.Background.Service.dll --HarbourBeatUrl=$HARBOURBEAT_API_URL
