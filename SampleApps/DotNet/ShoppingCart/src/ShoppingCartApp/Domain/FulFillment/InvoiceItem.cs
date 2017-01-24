using ShoppingCartApp.Domain.Currency;

namespace ShoppingCartApp.Domain.FulFillment
{
    public class InvoiceItem
    {
        public string Sku { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }

        public InvoiceItem(string sku, string description, Money price)
        {
            Sku = sku;
            Description = description;
            Price = price;
        }
    }
}