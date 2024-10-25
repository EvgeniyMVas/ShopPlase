using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPlase.Data;
using ShopPlase.Models;

namespace ShopPlase.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationContext _context;


        public SeedController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Count = _context.Products.Count();
            return View(_context.Products.Include(e => e.Category).OrderBy(e => e.Id).Take(20));
        }
        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                var categories = new List<(string Name, string Description)>
        {
            ("Игровые консоли", "Консоли для игр и развлечений"),
            ("Аксессуары", "Аксессуары для компьютеров и приставок"),
            ("Игры", "Игры для компьютеров и приставок"),
            ("Электроника", "Электронные устройства и гаджеты")
        };

                // Добавляем категории в базу данных
                foreach (var (name, description) in categories)
                {
                    var category = new Category { Name = name, Description = description };
                    _context.Categories.Add(category);
                }
                _context.SaveChanges();
                var productCategoryMapping = new Dictionary<string, string>
                {
                    { "PlayStation 5", "Игровые консоли" },
                    { "Xbox Series X", "Игровые консоли" },
                    { "Nintendo Switch", "Игровые консоли" },
                    { "Гарнитура HyperX", "Аксессуары" },
                    { "Клавиатура Logitech", "Аксессуары" },
                    { "Мышь Razer", "Аксессуары" },
                    { "Call of Duty", "Игры" },
                    { "FIFA 2024", "Игры" },
                    { "Good War", "Игры" },
                    { "CS 2", "Игры" },
                    { "Геймпад DualShock", "Аксессуары" },
                    { "Кабель HDMI 2.1", "Аксессуары" },
                    { "Acer 5XPro Game", "Игровые консоли" },
                    { "PlayStation 4", "Игровые консоли" },
                    { "Age Of Empires", "Игры" },
                    { "Смартфон Samsung Galaxy S22", "Электроника" },
                    { "Наушники Sony WH-1000XM5", "Электроника" },
                    { "Планшет Apple iPad Pro", "Электроника" },
                    { "Умные часы Apple Watch Series 8", "Электроника" },
                    { "Игровой ноутбук ASUS ROG Strix", "Электроника" }
                };

                var categoryEntities = _context.Categories.ToList();
                var random = new Random();

                foreach (var kvp in productCategoryMapping)
                {
                    var productName = kvp.Key;
                    var categoryName = kvp.Value;

                    // Находим категорию по названию
                    var category = categoryEntities.FirstOrDefault(c => c.Name == categoryName);
                    if (category == null)
                    {
                        continue; // Пропускаем, если категория не найдена
                    }

                    // Генерируем случайные цены
                    var purchasePrice = Math.Round((decimal)(random.NextDouble() * 500 + 10), 2); // от 10 до 510
                    var retailPrice = Math.Round(purchasePrice + (decimal)(random.NextDouble() * 100 + 20), 2); // от 20 до 120 к цене

                    // Добавляем продукт в базу данных с привязкой к правильной категории
                    var product = new Product
                    {
                        Name = productName,
                        CategoryId = category.Id,
                        PurchasePrice = purchasePrice,
                        RetailPrice = retailPrice
                    };

                    _context.Products.Add(product);
                }
                _context.SaveChanges();

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ClearData()
        {
            _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlRaw("DELETE FROM Products");
            _context.Database.ExecuteSqlRaw("DELETE FROM Categories");
            _context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

    }
}
