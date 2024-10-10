# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.sln ./
COPY Test_OP_Web/Test_OP_Web.csproj ./Test_OP_Web/
RUN dotnet restore

# Копируем остальные файлы и собираем проект
COPY . ./
RUN dotnet publish -c Release -o out

# Используем официальный образ .NET Runtime для выполнения приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Открываем порт для приложения
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Test_OP_Web.dll"]
