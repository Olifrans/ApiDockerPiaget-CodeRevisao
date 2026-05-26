FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj e restaura depend?ncias
COPY ["ApiDockerPiaget.csproj", "./"]
RUN dotnet restore "ApiDockerPiaget.csproj"

COPY . .
RUN dotnet publish "ApiDockerPiaget.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "ApiDockerPiaget.dll"]