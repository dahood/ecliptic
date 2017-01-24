using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartApp.Domain.Products
{
    public class ProductCatalog
    {
        public IEnumerable<Product> Products { get; private set; }

        public ProductCatalog(IEnumerable<Product> sellables)
        {
            Products = sellables;
        }

        public Product FindByName(string name)
        {
            return Products.First(x => x.Name == name);
        }

        public Product this[string sku]
        {
            get { return Products.First(x => x.Sku == sku); }
        }
    }
}
