using System;
using ShoppingCartApp.Domain.Currency;
using ShoppingCartApp.Domain.Products;

namespace ShoppingCartApp.Domain.Cart
{
    public class CartItem : IEquatable<CartItem>
    {
        public CartItem(Product product, int units)
        {
            Product = product;
            Units = units;
            Price = product.UnitPrice*Units;
        }

        public Product Product { get; set; }
        public Money Price { get; set; }
        public int Units { get; set; }

        public bool Equals(CartItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Product, other.Product) && Equals(Price, other.Price) && Units == other.Units;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CartItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Product?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Price?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ Units;
                return hashCode;
            }
        }

        public static bool operator ==(CartItem left, CartItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CartItem left, CartItem right)
        {
            return !Equals(left, right);
        }
    }
}