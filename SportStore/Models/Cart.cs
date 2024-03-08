namespace SportStore.Models
{
    // Класс для представления корзины покупок
    public class Cart
    {
        // Список элементов корзины
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        // Метод для добавления товара в корзину
        public void AddItem(Product product, int quantity)
        {
            // Проверяем, есть ли уже товар в корзине
            CartLine line = Lines.FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            if (line == null)
            {
                // Если товара нет в корзине, добавляем новую строку
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // Если товар уже есть в корзине, увеличиваем количество
                line.Quantity += quantity;
            }
        }

        // Метод для удаления товара из корзины
        public void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        // Метод для вычисления общей стоимости товаров в корзине
        public decimal ComputeTotalValue()
        {
            return Lines.Sum(e => e.Product.Price * e.Quantity);
        }

        // Метод для очистки корзины
        public void Clear()
        {
            Lines.Clear();
        }
    }

    // Класс для представления строки корзины
    public class CartLine
    {
        // Идентификатор строки корзины
        public int CartLineId { get; set; }

        // Товар
        public Product Product { get; set; }

        // Количество товара
        public int Quantity { get; set; }
    }
}
