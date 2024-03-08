using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    // Класс для заполнения базы данных начальными данными
    public class SeedData
    {
        // Метод для убеждения, что база данных заполнена начальными данными
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // Получение контекста базы данных через сервис приложения
            StoreDbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<StoreDbContext>();

            // Проверка наличия непримененных миграций
            if (context.Database.GetPendingMigrations().Any())
            {
                // Применение миграций, если они есть
                context.Database.Migrate();
            }

            // Проверка наличия продуктов в базе данных
            if (!context.Products.Any())
            {
                // Добавление начальных продуктов, если база данных пуста
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Каяк",
                        Description = "Лодка для одного человека",
                        Category = "Водный спорт",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Спортивный рюкзак",
                        Description = "Рюкзак для спортивного инвентаря",
                        Category = "Рюкзаки и сумки",
                        Price = 20
                    },
                    new Product
                    {
                        Name = "Футбольный мяч",
                        Description = "С размером и весом, утвержденными FIFA",
                        Category = "Футбол",
                        Price = 15
                    },
                    new Product
                    {
                        Name = "Зимняя куртка Puma",
                        Description = "Зимняя одежда",
                        Category = "Верхняя одежда",
                        Price = 35
                    },
                    new Product
                    {
                        Name = "Волейбольный мяч",
                        Description = "Стандартный волейбольный мяч",
                        Category = "Волейбол",
                        Price = 11
                    }
                );

                // Сохранение изменений в базе данных
                context.SaveChanges();
            }
        }
    }
}
