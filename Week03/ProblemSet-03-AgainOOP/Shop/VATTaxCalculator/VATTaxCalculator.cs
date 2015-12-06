using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATTaxCalculator
{
    public class TaxCalculator
    {
        private readonly List<CountryVatTax> countries;

        public TaxCalculator(List<CountryVatTax> countries)
        {
            this.countries = new List<CountryVatTax>(countries.Count);
            foreach (var country in countries)
            {
                this.countries.Add(country);
            }
        }

        public double CalculateTax(double itemPrice, int countryId)
        {
            foreach (var country in countries)
            {
                if (country.CountryId == countryId)
                {
                    return itemPrice + itemPrice * country.VATTax;
                }
            }
            var up = new NotSupportedCountryException("Id not found in the calculator database!");
            throw up;
        }

        public double CalculateTax(double itemPrice)
        {
            foreach (var country in countries)
            {
                if (country.IsDefault)
                {
                    return itemPrice + itemPrice * country.VATTax;
                }
            }
            var up = new NotSupportedCountryException("No default country in calculator database!");
            throw up;
        }

    }

    public class NotSupportedCountryException : Exception
    {
        public NotSupportedCountryException()
        {

        }

        public NotSupportedCountryException(string message)
            : base(message)
        {

        }
    }
}
