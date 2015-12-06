using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATTaxCalculator
{
    public class CountryVatTax
    {
        private readonly int countryId;
        private readonly double vatTax;
        private readonly bool isDefault;
        public int CountryId { get { return countryId; } }
        public double VATTax { get { return vatTax; } }
        public bool IsDefault { get { return isDefault; } }

        public CountryVatTax(int countryId, double vatTax, bool isDefault)
        {
            this.countryId = countryId;
            this.vatTax = vatTax;
            this.isDefault = isDefault;
        }
    }
}
