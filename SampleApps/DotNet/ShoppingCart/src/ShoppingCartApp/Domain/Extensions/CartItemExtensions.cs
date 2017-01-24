using System.Collections.Generic;
using System.Linq;
using ShoppingCartApp.Domain.Cart;
using ShoppingCartApp.Domain.Currency;

namespace ShoppingCartApp.Domain.Extensions
{
    public static class CartItemExtensions
    {
        public static Money Sum(this IEnumerable<CartItem> itemsInCart)
        {
            return new Money(itemsInCart.Sum(x => x.Price.Amount));
        }
    }
}
