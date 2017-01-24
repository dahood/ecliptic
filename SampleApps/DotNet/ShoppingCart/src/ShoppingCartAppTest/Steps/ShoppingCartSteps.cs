using System.Linq;
using FluentAssertions;
using ShoppingCartApp.Domain.Cart;
using ShoppingCartApp.Domain.Currency;
using ShoppingCartApp.Domain.FulFillment;
using ShoppingCartApp.Domain.Products;
using ShoppingCartApp.Tests.Extensions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ShoppingCartApp.Tests.Steps
{
    [Binding]
    public sealed class ShoppingCartSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        private static ShoppingCart MyCart
        {
            get { return ScenarioContext.Current.Get<ShoppingCart>(); }
            set { ScenarioContext.Current.Store(value); }
        }

        private static ProductCatalog ProductCatalog
        {
            get { return ScenarioContext.Current.Get<ProductCatalog>(); }
            set { ScenarioContext.Current.Store(value); }
        }

        private static Invoice MyInvoice
        {
            get { return ScenarioContext.Current.Get<Invoice>(); }
            set { ScenarioContext.Current.Store(value); }
        }

        [Given(@"I start with an Empty Shopping Cart")]
        public void GivenIStartWithAnEmptyShoppingCart()
        {
            MyCart = new ShoppingCart();
        }

        [Given(@"I have the following Products to sell")]
        public void GivenIHaveTheFollowingProductsToSell(Table table)
        {
            var products = table.Rows.Select(row =>  new Product(row["SKU"], row["Name"], new Money(row.GetDecimal("Unit Price"))));
            ProductCatalog = new ProductCatalog(products.ToList());
        }

        [Given(@"I add (.*) to the Shopping Cart")]
        public void GivenIAddProductToTheShoppingCart(string productName)
        {
            var product = ProductCatalog.FindByName(productName.Trim());
            MyCart.Add(product, 1);
        }

        [When(@"I go to checkout")]
        public void WhenIGoToCheckout()
        {
            MyInvoice = MyCart.CheckOut();
        }

        [Then(@"I should have the following in the shopping Cart")]
        public void ThenIShouldHaveTheFollowingInTheShoppingCart(Table table)
        {
            var invoice = MyInvoice;
            var actuals =invoice.InvoiceItems.Select(x => new { SKU = x.Sku, Item = x.Description, Price = x.Price });
            table.CompareToSet(actuals);
        }

        [Then(@"my total should be (.*)")]
        public void ThenMyTotalShouldBe(decimal expected)
        {
            MyInvoice.Total.Should().Be(new Money(expected));
        }
    }
}
