using System.Collections.Generic;
using ShoppingCartApp.Domain.Currency;

namespace ShoppingCartApp.Domain.FulFillment
{
    public class Invoice
    {
        public Money SubTotal { get; set; }
        public Money Tax { get; set; }
        public Money Total { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }

        public Invoice()
        {
            Initialize();
            InvoiceItems = new List<InvoiceItem>();
        }

        private void Initialize()
        {
            SubTotal = Money.Zero;
            Tax = Money.Zero;
            Total = Money.Zero;
        }

        public void Add(InvoiceItem invoiceItem)
        {
            InvoiceItems.Add(invoiceItem);
            Recalculate();
        }

        private void Recalculate()
        {
            Initialize();
            InvoiceItems.ForEach(each =>
            {
                SubTotal += each.Price;
                Total += each.Price;
            });
        }
    }
}
