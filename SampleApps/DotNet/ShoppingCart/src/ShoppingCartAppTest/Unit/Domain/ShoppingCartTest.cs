using FluentAssertions;
using NUnit.Framework;
using ShoppingCartApp.Domain.Cart;
using ShoppingCartApp.Domain.Currency;
using ShoppingCartApp.Domain.Products;

namespace ShoppingCartApp.Tests.Unit.Domain
{
    [TestFixture]
    public class ShoppingCartTest
    {
        private ShoppingCart cart;

        [SetUp]
        public void SetUp()
        {
            cart = new ShoppingCart();
        }

        [Test]
        public void AddItemToCart()
        {
            var product = new Product("sku001", "Milk", 2.25m);
            cart.Add(product, 2);

            cart.Total.Should().Be(new Money(4.5));
        }
    }
}
