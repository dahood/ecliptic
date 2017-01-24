using ShoppingCartApp.Domain.Currency;

namespace ShoppingCartApp.Domain.Products
{
    public class Product
    {
        public string Sku { get; private set; }
        public string Name { get; set; }
        public Money UnitPrice { get; set; }

        public Product(string sku, string name, Money price)
        {
            Sku = sku;
            Name = name;
            UnitPrice = price;
        }
    }
}