using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATTaxCalculator;

namespace Shop
{
    class Product
    {
        private readonly string productName;
        private readonly int productId;
        private readonly int country;
        private int quantity;
        private readonly double beforeTaxPrice;
        private readonly double afterTaxPrice;
        public int Id { get { return productId; } }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public double BeforeTaxPrice { get { return beforeTaxPrice; } }
        public double AfterTaxPrice { get { return afterTaxPrice; } }

        public Product(string productName, int productId, int country, int quantity, double beforeTaxPrice, TaxCalculator calc)
        {
            this.productName = productName;
            this.productId = productId;
            this.country = country;
            this.quantity = quantity;
            this.beforeTaxPrice = beforeTaxPrice;
            afterTaxPrice = calc.CalculateTax(beforeTaxPrice);
        }



    }
}