# Estágio base com dependências comuns
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src
COPY ["ChatBoard.API/ChatBoard.API.csproj", "ChatBoard.API/"]
COPY ["ChatBoard.DataBase/ChatBoard.DataBase.csproj", "ChatBoard.DataBase/"]
COPY ["ChatBoard.DTO/ChatBoard.DTO.csproj", "ChatBoard.DTO/"]
COPY ["ChatBoard.Service/ChatBoard.Service.csproj", "ChatBoard.Service/"]
RUN dotnet restore "ChatBoard.API/ChatBoard.API.csproj"
COPY . .

# Estágio de desenvolvimento - mantém todas as ferramentas do SDK
FROM base AS dev
WORKDIR /src/ChatBoard.API
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
# Executar em modo de desenvolvimento com hot reload
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://+:8080"]

# Estágio de build - compila a aplicação
FROM base AS build
WORKDIR /src/ChatBoard.API
RUN dotnet build "ChatBoard.API.csproj" -c Release -o /app/build

# Estágio de publicação - cria os artefatos para produção
FROM build AS publish
RUN dotnet publish "ChatBoard.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final para produção - usa apenas o runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ChatBoard.API.dll"]
