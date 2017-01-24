using System.Collections.Generic;
using ShoppingCartApp.Domain.Currency;
using ShoppingCartApp.Domain.Extensions;
using ShoppingCartApp.Domain.FulFillment;
using ShoppingCartApp.Domain.Products;

namespace ShoppingCartApp.Domain.Cart
{
    public class ShoppingCart
    {
        public Money Total { get; private set; }
        public List<CartItem> Items { get; private set; }

        public ShoppingCart()
        {
            Items = new List<CartItem>();
            Total = Money.Zero;
        }

        public void Add(Product product, int units)
        {
            var item = new CartItem(product, units);
            Items.Add(item);
            Total = Items.Sum();
        }

        public Invoice CheckOut()
        {
            var invoice = new Invoice();
            Items.ForEach(each=> invoice.Add(new InvoiceItem(each.Product.Sku, each.Product.Name, each.Price)));
            return invoice;
        }
    }
}
