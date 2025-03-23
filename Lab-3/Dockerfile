# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

# Клонируем репозиторий
RUN apk add --no-cache git && \
    git clone https://github.com/DianaNeumann/env-app-example.git . && \
    apk del git

WORKDIR /src/EnvApp

RUN dotnet restore
RUN dotnet publish -c Release -o /app /p:UseAppHost=false EnvApp.csproj

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine

RUN adduser -D appuser
USER appuser

WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "EnvApp.dll"]
