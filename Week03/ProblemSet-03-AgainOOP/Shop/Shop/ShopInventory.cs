using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ShopInventory
    {
        private List<Product> products;

        public ShopInventory(List<Product> products)
        {
            this.products = new List<Product>(products.Count);
            foreach (var product in products)
            {
                this.products.Add(product);
            }
        }

        public double Audit()
        {
            double total = 0;
            foreach (var product in products)
            {
                total += product.AfterTaxPrice * product.Quantity;
            }
            return total;
        }

        public double RequestOrder(Order order)
        {
            foreach (KeyValuePair<int,int> requestedItem in order.OrderedItems)
            {
                bool found = false;
                foreach (Product product in products)
                {
                    if (product.Id == requestedItem.Key)
                    {
                        if (product.Quantity >= requestedItem.Value)
                        {
                            found = true;
                        }
                    }
                }
                if (!found) throw new NotAvailableInInventoryException("Some items are unavailable!");
            }

            double price = 0;

            foreach (var requestedItem in order.OrderedItems)
            {
                foreach (var product in products)
                {
                    if (product.Id == requestedItem.Key)
                    {
                        product.Quantity = product.Quantity - requestedItem.Value;
                        price += requestedItem.Value * product.AfterTaxPrice;
                    }
                }
            }
            return price;
        }
    }

    public class NotAvailableInInventoryException : Exception
    {
        public NotAvailableInInventoryException()
        {

        }

        public NotAvailableInInventoryException(string message)
            : base(message)
        {

        }
    }
}
