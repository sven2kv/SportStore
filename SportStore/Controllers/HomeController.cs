using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace SportStore.Controllers
{
    // Контроллер для обработки запросов связанных с домашней страницей и приватностью
    public class HomeController : Controller
    {
        private IStoreRepositiry repositiry;

        // Конструктор контроллера
        public HomeController(IStoreRepositiry repositiry)
        {
            // Внедрение зависимости через интерфейс IStoreRepository
            this.repositiry = repositiry;
        }

        // Количество элементов на странице
        public int PageSize = 4;

        // Метод для обработки запросов на главную страницу
        public ViewResult Index(string category, int productPage = 1)
        {
            // Формирование модели представления для передачи в представление
            return View(new ProductsListViewModel
            {
                // Получение списка продуктов с учетом фильтрации по категории и пагинацией
                Products = repositiry.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                // Формирование информации о пагинации
                PaginInfo = new PaginInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repositiry.Products.Count()
                }
            });
        }

        // Метод для отображения страницы с информацией о приватности
        public IActionResult Privacy()
        {
            return View();
        }

        // Метод для отображения страницы с сообщением об ошибке
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Формирование модели представления для страницы с ошибкой
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
