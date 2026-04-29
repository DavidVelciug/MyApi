# Backend

## Требования
- .NET SDK 10.0
- SQL Server (LocalDB/Express/Developer)

## Настройка базы данных
1. Откройте `MyFullstackApp.DataAccess/Context/AppDbContext.cs` и проверьте строку подключения.
2. При необходимости задайте connection string через переменные окружения или `appsettings`.
3. Примените миграции:

```bash
dotnet ef database update --project MyFullstackApp.DataAccess --startup-project MyFullstackApp.Api
```

## Запуск API
```bash
dotnet build
dotnet run --project MyFullstackApp.Api
```

По умолчанию API поднимается на адресе, который выводит `dotnet run` (обычно `http://localhost:5000`/`https://localhost:5001`).

## Важные seed-аккаунты
- `admin.one@memorylane.com` / `AdminOne123!` (admin)
- `admin.two@memorylane.com` / `AdminTwo123!` (admin)

Они автоматически создаются/обновляются при старте приложения.
