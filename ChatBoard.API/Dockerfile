FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src
COPY ["ChatBoard.API/ChatBoard.API.csproj", "ChatBoard.API/"]
COPY ["ChatBoard.Infrastructure/ChatBoard.Infrastructure.csproj", "ChatBoard.Infrastructure/"]
COPY ["ChatBoard.Domain/ChatBoard.Domain.csproj", "ChatBoard.Domain/"]
COPY ["ChatBoard.Application/ChatBoard.Application.csproj", "ChatBoard.Application/"]
RUN dotnet restore "ChatBoard.API/ChatBoard.API.csproj"
COPY . .

FROM base AS dev
WORKDIR /src/ChatBoard.API
EXPOSE 8080
# Executar em modo de desenvolvimento com hot reload
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://+:8080"]

FROM base AS build
WORKDIR /src/ChatBoard.API
RUN dotnet build "ChatBoard.API.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "ChatBoard.API.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS migrator
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBoard.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ChatBoard.API.dll"]
