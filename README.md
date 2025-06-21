# 🗪 ChatBoard API
ChatBoard é uma aplicação de chat em tempo real desenvolvida com .NET 8, utilizando SignalR para comunicação em tempo real e PostgreSQL como banco de dados.

## ✨Funcionalidades

- Chat em tempo real

- Grupos de conversa

- Persistência de mensagens

- Monitoramento de saúde (Health Checks)

- CORS configurado para aplicação Angular

## 🛠️ Tecnologias
- .NET 8

- SignalR

- Entity Framework Core

- PostgreSQL

- Angular (Frontend)

Exemplo de comando para executar o compose da aplicação (Rodar na raiz do projeto):
```bash
docker compose -f docker-compose.yml -f docker-compose.staging.yml --env-file ChatBoard.API/.env.staging up
```