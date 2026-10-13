using GenchoFit.Models;

namespace GenchoFit.Data;

public static class SeedData
{
    public static void AddProducts(ShopDbContext db)
    {
        if (db.Products.Any()) return;
        db.Products.AddRange(
            new Product { Name = "Whey протеин", Category = "Протеин", Price = 49.90m, Description = "Суроватъчен протеин с ванилов вкус – 1 кг.", Emoji = "🥛" },
            new Product { Name = "Креатин монохидрат", Category = "Сила", Price = 29.90m, Description = "Чист креатин за ежедневна тренировка – 300 г.", Emoji = "⚡" },
            new Product { Name = "Мултивитамини", Category = "Витамини", Price = 19.90m, Description = "Базов комплекс от витамини и минерали – 60 капсули.", Emoji = "💊" },
            new Product { Name = "Предтренировъчен бустер", Category = "Енергия", Price = 34.90m, Description = "Енергия и фокус преди тренировка – 300 г.", Emoji = "🔥" },
            new Product { Name = "Омега 3", Category = "Здраве", Price = 24.90m, Description = "Рибено масло с EPA и DHA – 90 капсули.", Emoji = "🐟" }
        );
        db.SaveChanges();
    }
}
