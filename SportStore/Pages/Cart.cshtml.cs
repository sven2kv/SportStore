using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportStore.Infrastructure;
using SportStore.Models;

namespace SportStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepositiry repositiry;

        // Конструктор класса CartModel
        public CartModel(IStoreRepositiry repositiry)
        {
            // Внедрение зависимости через интерфейс IStoreRepository
            this.repositiry = repositiry;
        }

        // Свойство для хранения корзины
        public Cart Cart { get; set; }

        // Свойство для хранения URL возврата
        public string ReturnUrl { get; set; }

        // Метод OnGet выполняется при запросе HTTP GET
        public void OnGet(string returnUrl)
        {
            // Устанавливаем URL возврата
            ReturnUrl = returnUrl ?? "/";
            // Получаем корзину из сеанса, если она существует, иначе создаем новую корзину
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        // Метод OnPost выполняется при запросе HTTP POST
        public IActionResult OnPost(long productId, string returnUrl)
        {
            // Получаем информацию о продукте по его идентификатору
            Product product = repositiry.Products.FirstOrDefault(p => p.ProductID == productId);
            // Получаем корзину из сеанса, если она существует, иначе создаем новую корзину
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            // Добавляем продукт в корзину
            Cart.AddItem(product, 1);
            // Сохраняем корзину в сессии
            HttpContext.Session.SetJson("cart", Cart);
            // Перенаправляем пользователя на страницу, с которой он пришел
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
