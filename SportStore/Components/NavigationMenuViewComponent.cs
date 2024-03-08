using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Components
{
    // Компонент представления для отображения навигационного меню
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepositiry repositiry;

        // Конструктор компонента представления
        public NavigationMenuViewComponent(IStoreRepositiry repositiry)
        {
            // Внедрение зависимости через интерфейс IStoreRepository
            this.repositiry = repositiry;
        }

        // Метод для вызова компонента представления
        public IViewComponentResult Invoke()
        {
            // Установка выбранной категории из маршрута
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            // Получение списка категорий продуктов и передача его в представление
            return View(repositiry.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
