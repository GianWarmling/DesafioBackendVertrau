# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY ./DesafioBackendVertrau/*.csproj ./DesafioBackendVertrau/
RUN dotnet restore

COPY . .
WORKDIR /src/DesafioBackendVertrau
RUN dotnet publish -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DesafioBackendVertrau.dll"]