# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS final

# Создание непривилегированного пользователя
RUN adduser -D appuser
USER appuser

WORKDIR /app
COPY --from=build /app/publish .

# Очистка временных данных
RUN rm -rf /tmp/* /var/cache/apk/*

ENTRYPOINT ["dotnet", "WebApiMongoDbSample.dll"]
