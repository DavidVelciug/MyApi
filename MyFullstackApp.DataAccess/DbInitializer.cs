using MyFullstackApp.DataAccess.Context;
using MyFullstackApp.Domains.Entities.Capsule;
using MyFullstackApp.Domains.Entities.Category;
using MyFullstackApp.Domains.Entities.Moderation;
using MyFullstackApp.Domains.Entities.Product;
using MyFullstackApp.Domains.Entities.User;
using MyFullstackApp.Domains.Enums;

namespace MyFullstackApp.DataAccess;

public static class DbInitializer
{
    public static void SeedIfEmpty(AppDbContext db)
    {
        if (!db.UserAccounts.Any())
        {
            SeedUsersCapsulesAndReports(db);
        }

        EnsureAdminAccounts(db);

        var admin = db.UserAccounts.First(u => u.Email == "admin.one@memorylane.com");

        if (!db.Categories.Any())
        {
            SeedCatalog(db, admin.Id, admin.Email);
        }
        else if (db.Products.Any(p => p.CapsuleId == null))
        {
            LinkCatalogProducts(db, admin.Id);
        }
    }

    private static void EnsureAdminAccounts(AppDbContext db)
    {
        EnsureAdmin(
            db,
            "admin.one@memorylane.com",
            "Главный админ",
            "AdminOne123!");
        EnsureAdmin(
            db,
            "admin.two@memorylane.com",
            "Резервный админ",
            "AdminTwo123!");
        db.SaveChanges();
    }

    private static void EnsureAdmin(AppDbContext db, string email, string displayName, string password)
    {
        var existing = db.UserAccounts.FirstOrDefault(x => x.Email == email);
        if (existing == null)
        {
            db.UserAccounts.Add(new UserAccountData
            {
                Email = email,
                DisplayName = displayName,
                Role = "admin",
                Password = password,
                CreatedAtUtc = DateTime.UtcNow,
                NotifyEmailEnabled = true,
                NotifyPushEnabled = true,
                LoginAlertsEnabled = true
            });
            return;
        }

        existing.Role = "admin";
        existing.Password = password;
        existing.DisplayName = displayName;
    }

    private static void SeedCatalog(AppDbContext db, int adminUserId, string adminEmail)
    {
        var personal = new CategoryData { Name = "Личное" };
        var dreams = new CategoryData { Name = "Мечты" };
        var publicCat = new CategoryData { Name = "Публичное" };
        db.Categories.AddRange(personal, dreams, publicCat);
        db.SaveChanges();

        var now = DateTime.UtcNow;
        var pastOpen = now.AddDays(-2);

        var cap1 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Послание потомкам",
            TextContent = "Привет из 2024 года! Мы живём в удивительное время — технологии развиваются стремительно, мир меняется каждый день. Надеюсь, что к тому моменту, как ты читаешь это, человечество стало добрее и мудрее. Берегите планету и друг друга!",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-60),
            RecipientEmail = adminEmail, IsPublic = false
        };
        var cap2 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Письмо в 2030 год",
            TextContent = "Мои цели на десятилетие:\n\n1. Научиться играть на гитаре\n2. Посетить не менее 10 стран\n3. Написать книгу\n4. Выучить испанский язык\n5. Пробежать марафон\n\nНадеюсь, к 2030 году я смогу осуществить всё задуманное! Если ты читаешь это — напомни мне, что я обещал себе это.",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-180),
            RecipientEmail = adminEmail, IsPublic = false
        };
        var cap3 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Мечты о космосе",
            TextContent = "Когда-нибудь человество обязательно полетит на Марс. Я мечтаю увидеть этот момент своими глазами. Представляю, как стою на красной планете и смотрю на Землю в иллюминатор. До встречи на Марсе!",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-90),
            RecipientEmail = adminEmail, IsPublic = false
        };
        var cap4 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Секретный рецепт",
            TextContent = "Бабушкин пирог с яблоками:\n\nТесто:\n- Мука 300г\n- Масло сливочное 150г\n- Сахар 100г\n- Яйцо 1шт\n- Щепотка соли\n\nНачинка:\n- Яблоки 4шт\n- Корица 1ч.л.\n- Сахар 2ст.л.\n\nВыпекать 40 минут при 180°C.\n\nСекретный ингредиент — любовь!",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-365),
            RecipientEmail = adminEmail, IsPublic = false
        };
        var cap5 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Капсула времени 2024",
            TextContent = "События 2024 года, которые нельзя забыть:\n\n- Технологический бум: ИИ проник во все сферы жизни\n- Космические миссии: новые открытия на Луне и Марсе\n- Спортивные рекорды: невероятные достижения на Олимпиаде\n- Культурные события: фильмы, музыка и искусство, которые объединили миллионы\n\nЭтот год навсегда останется в наших сердцах.",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-45),
            RecipientEmail = adminEmail, IsPublic = true
        };
        var cap6 = new TimeCapsuleData
        {
            OwnerUserId = adminUserId, ContentType = CapsuleContentType.Text,
            Title = "Путешествие в будущее",
            TextContent = "Маршрут моей мечты:\n\nДень 1-3: Токио, Япония\nДень 4-6: Киото, Япония\nДень 7-10: Бангкок, Таиланд\nДень 11-14: Бали, Индонезия\nДень 15-18: Сидней, Австралия\nДень 19-21: Новая Зеландия\n\nОднажды я обязательно отправлюсь в это путешествие!",
            OpenAtUtc = pastOpen, CreatedAtUtc = now.AddDays(-120),
            RecipientEmail = adminEmail, IsPublic = false
        };

        db.TimeCapsules.AddRange(cap1, cap2, cap3, cap4, cap5, cap6);
        db.SaveChanges();

        db.Products.AddRange(
            new ProductData { Name = "Послание потомкам", Price = 2999, CapsuleId = cap1.Id, CategoryId = personal.Id, Description = "Как мы жили в 2024 году", Image = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=500" },
            new ProductData { Name = "Письмо в 2030 год", Price = 1999, CapsuleId = cap2.Id, CategoryId = personal.Id, Description = "Мои цели на десятилетие", Image = "https://images.unsplash.com/photo-1484807352052-23338990c6c6?w=500" },
            new ProductData { Name = "Мечты о космосе", Price = 3999, CapsuleId = cap3.Id, CategoryId = dreams.Id, Description = "Записка о полете на Марс", Image = "https://images.unsplash.com/photo-1451187580459-43490279c0fa?w=500" },
            new ProductData { Name = "Секретный рецепт", Price = 1499, CapsuleId = cap4.Id, CategoryId = personal.Id, Description = "Бабушкин пирог", Image = "https://images.unsplash.com/photo-1556910103-1c02745aae4d?w=500" },
            new ProductData { Name = "Капсула времени 2024", Price = 4999, CapsuleId = cap5.Id, CategoryId = publicCat.Id, Description = "События этого года", Image = "https://images.unsplash.com/photo-1461360228754-6e81c478b882?w=500" },
            new ProductData { Name = "Путешествие в будущее", Price = 2499, CapsuleId = cap6.Id, CategoryId = dreams.Id, Description = "Маршрут моей мечты", Image = "https://images.unsplash.com/photo-1488085061387-422e29b40080?w=500" });

        db.SaveChanges();
    }

    private static void LinkCatalogProducts(AppDbContext db, int adminUserId)
    {
        var adminEmail = db.UserAccounts.First(u => u.Id == adminUserId).Email;
        var now = DateTime.UtcNow;
        var pastOpen = now.AddDays(-2);

        var capsuleTexts = new Dictionary<string, string>
        {
            ["Послание потомкам"] = "Привет из 2024 года! Мы живём в удивительное время — технологии развиваются стремительно, мир меняется каждый день. Надеюсь, что к тому моменту, как ты читаешь это, человечество стало добрее и мудрее. Берегите планету и друг друга!",
            ["Письмо в 2030 год"] = "Мои цели на десятилетие:\n\n1. Научиться играть на гитаре\n2. Посетить не менее 10 стран\n3. Написать книгу\n4. Выучить испанский язык\n5. Пробежать марафон\n\nНадеюсь, к 2030 году я смогу осуществить всё задуманное! Если ты читаешь это — напомни мне, что я обещал себе это.",
            ["Мечты о космосе"] = "Когда-нибудь человество обязательно полетит на Марс. Я мечтаю увидеть этот момент своими глазами. Представляю, как стою на красной планете и смотрю на Землю в иллюминатор. До встречи на Марсе!",
            ["Секретный рецепт"] = "Бабушкин пирог с яблоками:\n\nТесто:\n- Мука 300г\n- Масло сливочное 150г\n- Сахар 100г\n- Яйцо 1шт\n- Щепотка соли\n\nНачинка:\n- Яблоки 4шт\n- Корица 1ч.л.\n- Сахар 2ст.л.\n\nВыпекать 40 минут при 180°C.\n\nСекретный ингредиент — любовь!",
            ["Капсула времени 2024"] = "События 2024 года, которые нельзя забыть:\n\n- Технологический бум: ИИ проник во все сферы жизни\n- Космические миссии: новые открытия на Луне и Марсе\n- Спортивные рекорды: невероятные достижения на Олимпиаде\n- Культурные события: фильмы, музыка и искусство, которые объединили миллионы\n\nЭтот год навсегда останется в наших сердцах.",
            ["Путешествие в будущее"] = "Маршрут моей мечты:\n\nДень 1-3: Токио, Япония\nДень 4-6: Киото, Япония\nДень 7-10: Бангкок, Таиланд\nДень 11-14: Бали, Индонезия\nДень 15-18: Сидней, Австралия\nДень 19-21: Новая Зеландия\n\nОднажды я обязательно отправлюсь в это путешествие!"
        };

        var productsWithoutCapsule = db.Products.Where(p => p.CapsuleId == null).ToList();

        foreach (var product in productsWithoutCapsule)
        {
            var text = capsuleTexts.GetValueOrDefault(product.Name, $"Содержимое капсулы \"{product.Name}\". Откройте её, чтобы узнать больше!");
            var capsule = new TimeCapsuleData
            {
                OwnerUserId = adminUserId,
                ContentType = CapsuleContentType.Text,
                Title = product.Name,
                TextContent = text,
                OpenAtUtc = pastOpen,
                CreatedAtUtc = now,
                RecipientEmail = adminEmail,
                IsPublic = product.Name == "Капсула времени 2024"
            };
            db.TimeCapsules.Add(capsule);
            db.SaveChanges();
            product.CapsuleId = capsule.Id;
        }

        db.SaveChanges();
    }

    private static void SeedUsersCapsulesAndReports(AppDbContext db)
    {
        var u1 = new UserAccountData
        {
            Email = "demo@memorylane.com",
            DisplayName = "Демо пользователь",
            Role = "user",
            Password = "demo123",
            CreatedAtUtc = DateTime.UtcNow.AddDays(-30),
            NotifyEmailEnabled = true,
            NotifyPushEnabled = true,
            LoginAlertsEnabled = true
        };
        var u2 = new UserAccountData
        {
            Email = "maria@example.com",
            DisplayName = "Мария",
            Role = "moderator",
            Password = "maria123",
            CreatedAtUtc = DateTime.UtcNow.AddDays(-14),
            NotifyEmailEnabled = true,
            NotifyPushEnabled = false,
            LoginAlertsEnabled = true
        };
        db.UserAccounts.AddRange(u1, u2);
        db.SaveChanges();

        var now = DateTime.UtcNow;
        var pastOpen = now.AddDays(-2);
        var futureOpen = now.AddDays(30);

        var capSealed = new TimeCapsuleData
        {
            OwnerUserId = u1.Id,
            ContentType = CapsuleContentType.Text,
            Title = "Личное письмо будущему",
            TextContent = "Содержимое скрыто до даты открытия.",
            OpenAtUtc = futureOpen,
            CreatedAtUtc = now.AddDays(-5),
            RecipientEmail = "demo@memorylane.com",
            IsPublic = false
        };

        var capLink = new TimeCapsuleData
        {
            OwnerUserId = u2.Id,
            ContentType = CapsuleContentType.Link,
            Title = "Ссылка на воспоминание",
            LinkUrl = "https://memorylane.example.com/story/1",
            OpenAtUtc = pastOpen,
            CreatedAtUtc = now.AddDays(-3),
            RecipientEmail = u2.Email,
            IsPublic = true
        };

        db.TimeCapsules.AddRange(capSealed, capLink);
        db.SaveChanges();

        db.ModerationReports.Add(new ModerationReportData
        {
            CapsuleId = capLink.Id,
            ReporterEmail = "moderator@memorylane.com",
            Reason = "Подозрение на спам в публичной ленте",
            Status = ReportStatus.Open,
            CreatedAtUtc = now.AddDays(-1)
        });

        db.SaveChanges();
    }
}
