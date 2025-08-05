# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY *.sln .
COPY FIAP.CloudGames.API/*.csproj ./FIAP.CloudGames.API/
COPY FIAP.CloudGames.Application/*.csproj ./FIAP.CloudGames.Application/
COPY FIAP.CloudGames.Domain/*.csproj ./FIAP.CloudGames.Domain/
COPY FIAP.CloudGames.Infrastructure/*.csproj ./FIAP.CloudGames.Infrastructure/
COPY FIAP.CloudGames.Tests/*.csproj ./FIAP.CloudGames.Tests/
RUN dotnet restore

# Copia tudo e publica
COPY . .
WORKDIR /app/FIAP.CloudGames.API
RUN dotnet publish -c Release -o out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/FIAP.CloudGames.API/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "FIAP.CloudGames.API.dll"]
