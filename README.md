# GenchoFit

Малък ASP.NET Core MVC магазин за хранителни добавки с локален MySQL сървър и phpMyAdmin.

Има и базова система за клиентски акаунти: регистрация, вход, logout и профил. Паролите се записват като hash в таблицата `Users`.

## Стартиране

1. Инсталирай .NET 8 SDK и XAMPP (MySQL + phpMyAdmin) или Docker.
2. Стартирай MySQL на порт `3306`.
3. В phpMyAdmin изпълни `database/init.sql` или просто остави приложението да създаде базата.
4. Провери паролата в `appsettings.json` (по подразбиране е празна за XAMPP root).
5. От папката `GenchoFit` изпълни:

```bash
dotnet restore
dotnet run
```

Отвори адреса, който конзолата покаже. При първо пускане се създават таблицата и примерните продукти.

Това е учебен демо проект: количката и плащането са само визуални, няма реална поръчка или картови данни.

## Какво има в проекта

- `Controllers/ShopController.cs` — каталог, търсене и детайли на продукт.
- `Controllers/AccountController.cs` — регистрация, вход, профил и logout.
- `Data/ShopDbContext.cs` — връзка към MySQL и таблиците `Products`/`Users`.
- `Views/` — простите Razor страници на магазина.
- `wwwroot/` — CSS и локалната фитнес снимка.
- `database/init.sql` — създаване на базата през phpMyAdmin.

## Акаунти

При регистрация се създава запис в `Users`, а входът използва cookie authentication. За реален production сайт трябва да се добавят rate limiting, email verification, password reset и по-силен password hashing provider.
