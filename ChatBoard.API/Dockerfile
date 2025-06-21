FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src
COPY ["ChatBoard.API/ChatBoard.API.csproj", "ChatBoard.API/"]
COPY ["ChatBoard.DataBase/ChatBoard.DataBase.csproj", "ChatBoard.DataBase/"]
COPY ["ChatBoard.DTO/ChatBoard.DTO.csproj", "ChatBoard.DTO/"]
COPY ["ChatBoard.Services/ChatBoard.Services.csproj", "ChatBoard.Services/"]
RUN dotnet restore "ChatBoard.API/ChatBoard.API.csproj" \
    && dotnet restore "ChatBoard.DataBase/ChatBoard.DataBase.csproj" \
    && dotnet restore "ChatBoard.DTO/ChatBoard.DTO.csproj" \
    && dotnet restore "ChatBoard.Services/ChatBoard.Services.csproj"
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

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ChatBoard.API.dll"]
