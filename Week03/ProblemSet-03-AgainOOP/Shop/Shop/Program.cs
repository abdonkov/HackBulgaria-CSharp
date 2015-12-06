using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATTaxCalculator;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {

            CountryVatTax[] countries = new CountryVatTax[]
            {
                new CountryVatTax(1, 0.2, true),
                new CountryVatTax(2, 0.3, false),
                new CountryVatTax(3, 0.22, false),
                new CountryVatTax(4, 0.17, false),
                new CountryVatTax(5, 0.43, false),
                new CountryVatTax(6, 0.13, false)
            };

            TaxCalculator calc = new TaxCalculator(countries.ToList());

            Product[] products = new Product[]
            {
                new Product("Hlqb"  , 1, 1, 7, 0.8, calc),
                new Product("Sirene", 2, 1, 3, 4.5, calc),
                new Product("Voda"  , 3, 1, 5, 0.4, calc),
                new Product("Qica"  , 4, 1, 10, 0.1, calc),
                new Product("Emeka" , 5, 1, 2, 2.5, calc),
                new Product("Sok"   , 6, 1, 1, 2.2, calc)                
            };

            ShopInventory shop = new ShopInventory(products.ToList());

            Dictionary<int, int> items1 = new Dictionary<int, int>();
            items1.Add(2, 2);
            items1.Add(3, 3);
            items1.Add(1, 2);
            Order order1 = new Order(items1);

            Dictionary<int, int> items2 = new Dictionary<int, int>();
            items2.Add(4, 8);
            items2.Add(5, 1);
            items2.Add(3, 1);
            Order order2 = new Order(items2);

            Dictionary<int, int> items3 = new Dictionary<int, int>();
            items3.Add(2, 1);
            items3.Add(5, 1);
            items3.Add(3, 2);
            Order order3 = new Order(items3);

            try
            {
                Console.WriteLine("Audit: {0}", shop.Audit());
                Console.WriteLine("Order 1 value: {0}", shop.RequestOrder(order1));
                Console.WriteLine("Audit: {0}", shop.Audit());
                Console.WriteLine("Order 2 value: {0}", shop.RequestOrder(order2));
                Console.WriteLine("Audit: {0}", shop.Audit());
                Console.WriteLine("Order 3 value: {0}", shop.RequestOrder(order3));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }

            Console.WriteLine("Audit: {0}", shop.Audit());

            Console.ReadKey();
        }
    }
}
